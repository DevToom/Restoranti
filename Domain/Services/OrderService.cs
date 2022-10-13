using Domain.Interface;
using Entities.Entities;
using Entities.Entities.VM;
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

        public OrderService(IROrder rOrder)
        {
            this._rOrder = rOrder;
        }

        public async Task<List<Order>> GetListOrdersAsync()
        {
            var orderList = await _rOrder.GetList();

            return orderList;
        }

        Task<MessageResponse<Order>> PostOrderAsync(Order request)
        {
            try
            {
                if (request != null)
                {

                    Order order = new Order()
                    {
                        UserId = request.UserId,
                        TableNumber = request.TableNumber,
                        Total = request.Total
                    };

                    //foreach (var i in request.itens)
                    //{
                    //    order.Itens.Add(new Itens
                    //    {
                    //        ProductId = i.ProductId,
                    //        Name = i.Name,
                    //        CategoryId = i.CategoryId,
                    //        Value = i.Value
                    //    });
                    //}

                    await _rOrder.AddAsync(order);

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
