using Domain.Interface;
using Entities.Entities;
using Entities.Entities.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{


    public class OrderService : IOrderService
    {
        private readonly IROrder _rOrder;
        private readonly ITableService _tableService;
        private readonly IAccountOrderService _accountOrderService;

        public OrderService(IROrder rOrder, ITableService tableService, IAccountOrderService accountOrderService)
        {
            this._rOrder = rOrder;
            this._tableService = tableService;
            this._accountOrderService = accountOrderService;
        }

        public async Task<MessageResponse<List<OrderResponseVM>>> GetListOrdersAsync()
        {
            try
            {
                List<OrderResponseVM> listFinally = new List<OrderResponseVM>();
                OrderResponseVM order = new OrderResponseVM();

                var orderList = await _rOrder.GetList();
                var OrderNumbers = orderList.Select(x => x.OrderNumber).Distinct().ToList();

                foreach (var orderNumber in OrderNumbers)
                {
                    var listOrder = _rOrder.GetByOrderNumber(orderNumber).Result;

                    if (listOrder?.Count > 0)
                    {
                        order = new OrderResponseVM();
                        order.OrderNumber = listOrder.FirstOrDefault().OrderNumber;
                        order.TableNumber = listOrder.FirstOrDefault().TableNumber;
                        order.UserId = listOrder.FirstOrDefault().UserId;
                        order.Type = listOrder.FirstOrDefault().Type;
                        order.Status = listOrder.FirstOrDefault().Status;
                        order.Total = listOrder.FirstOrDefault().Total;
                        order.Products = new List<ProductVM>();

                        foreach (var o in listOrder)
                        {

                            order.Products.Add(new ProductVM
                            {
                                ProductId = o.ProductId,
                                ProductName = o.ProductName,
                                Quantity = o.Quantity,
                                Value = o.Value,
                                hasObservation = o.hasObservation,
                                Observation = o.Observation
                            });

                            if (o.hasObservation)
                                order.hasObservation = true;
                        }

                        listFinally.Add(order);
                    }

                }

                return new MessageResponse<List<OrderResponseVM>> { HasError = false, Entity = listFinally };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<OrderResponseVM>> { HasError = true, Message = $"Ocorreu um erro técnico ao tentar buscar lista de pedidos. Exception: {ex.Message}" };
            }
        }

        public async Task<MessageResponse<List<Order>>> PostOrderAsync(OrderVM request)
        {
            try
            {
                if (request != null)
                {
                    if (request.Product?.Count > 0)
                    {
                        string OrderNumber = String.Empty;
                        bool HasUpdateTable = true;
                        bool HasOpenedAccount = true;

                        try
                        {
                            //Quando for o primeiro pedido, abrir uma mesa e também abrir a conta para o cliente
                            if (request.isFirst && request.AccountOrderId == 0)
                            {
                                HasUpdateTable = await _tableService.UpdateStatusTable(request.TableNumber);
                                var result = await _accountOrderService.OpenAccount(request.TableNumber, request.UserId);
                                HasOpenedAccount = result.HasError == true ? false : true;
                                if (HasOpenedAccount)
                                    request.AccountOrderId = result.Entity.Id;
                            }

                            List<Order> list = new List<Order>();
                            OrderNumber = GenerateOrderNumber();
                            DateTime creationDate = DateTime.Now;

                            if (HasUpdateTable && HasOpenedAccount)
                            {
                                foreach (var item in request.Product)
                                {
                                    Order order = new Order()
                                    {
                                        OrderNumber = OrderNumber,
                                        TableNumber = request.TableNumber,
                                        UserId = request.UserId,
                                        Type = request.Type,
                                        Status = EStatus.A_PREPARAR,
                                        ProductId = item.ProductId,
                                        ProductName = item.ProductName,
                                        Quantity = item.Quantity,
                                        Value = item.Value,
                                        hasObservation = item.hasObservation,
                                        Observation = item.Observation,
                                        Total = request.Total,
                                        CreationDate = creationDate,
                                        CreationUserId = request.UserId,
                                        AccountOrderId = request.AccountOrderId
                                    };

                                    await _rOrder.AddAsync(order);
                                    list.Add(order);
                                }

                                //Atualizar o valor total da conta.
                                await _accountOrderService.UpdateValueTotalAccount(request);

                                return new MessageResponse<List<Order>> { HasError = false, Entity = list, Message = $"Pedido gerado com sucesso!" };
                            }
                            else
                                return new MessageResponse<List<Order>> { HasError = true, Entity = list, Message = $"Houve um problema ao tentar abrir a mesa e/ou a conta, chame um funcionário!" };
                        }
                        catch (Exception ex)
                        {
                            return new MessageResponse<List<Order>> { HasError = true, Message = $"Ocorreu um erro técnico ao tentar adicionar o pedido {OrderNumber}. Exception: {ex.Message}" };
                        }
                    }
                    else
                        return new MessageResponse<List<Order>> { HasError = true, Message = $"Pedido não contém nenhum produto! JSON-> {JsonConvert.SerializeObject(request)}" };
                }
                else
                    return new MessageResponse<List<Order>> { HasError = true, Message = $"Pedido nulo" };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Order>> { HasError = true, Message = $"Ocorreu um erro técnico ao tentar buscar lista de pedidos. Exception: {ex.Message}" };
            }
        }

        /// <summary>
        /// Gerar número do pedido
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateOrderNumber(int length = 10)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
