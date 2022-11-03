using Domain.Interface;
using Entities.Entities;
using Entities.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TableService : ITableService
    {
        private readonly IRTable _rTable;
        private readonly IAccountOrderService _accountOrderService;
        private readonly IRAccountOrder _rAccountOrder;
        public TableService(IRTable rTable, IAccountOrderService accountOrderService, IRAccountOrder rAccountOrder)
        {
            this._rTable = rTable;
            this._accountOrderService = accountOrderService;
            _rAccountOrder = rAccountOrder;
        }

        public async Task<MessageResponse<Table>> Add(Table request)
        {
            try
            {
                if (request != null)
                {
                    var tables = _rTable.GetList().Result;
                    if (tables.Any())
                    {
                        var tableNumber = tables.OrderByDescending(x => x.CreationDate).ToList().FirstOrDefault().TableNumber;
                        request.TableNumber = tableNumber + 1;
                    }
                    else
                        request.TableNumber = 1;

                    request.IsActive = true;
                    request.TableStatus = TableConstants.OPEN_VALUE;

                    await _rTable.AddAsync(request);
                    return new MessageResponse<Table> { Entity = request, Message = "Mesa criada com sucesso!" };
                }
                else
                {
                    return new MessageResponse<Table> { HasError = true, Message = "Não há nenhuma informação sobre a nova mesa. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Table> { HasError = true, Message = $"Não possível adicionar uma nova mesa - {ex.Message}" };
            }
        }

        public async Task<MessageResponse<List<Table>>> GetList()
        {
            try
            {
                var entityList = await _rTable.GetList();
                return new MessageResponse<List<Table>> { Entity = entityList };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Table>> { HasError = true, Message = $"Ocorreu um problema ao tentar buscar as informações de mesas. Ex: {ex.Message}" };
            }
        }

        public async Task<bool> UpdateStatusTable(int TableNumber)
        {
            try
            {
                if (TableNumber > 0)
                {
                    var tables = await _rTable.GetList();
                    if (tables.Any())
                    {
                        var table = tables.Where(x => x.TableNumber == TableNumber).FirstOrDefault();
                        if (table != null)
                        {
                            table.TableStatus = TableConstants.BLOCK_VALUE;
                            await _rTable.Update(table);
                        }
                        else
                            return false;
                    }
                }
                else
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MessageResponse<List<Table>>> GetListByFilters(int TableNumber, string Status)
        {
            try
            {
                var entityList = await _rTable.GetList();
                entityList = entityList.Where(x => x.TableStatus.ToUpper().Contains(Status.ToUpper()) && x.TableNumber == TableNumber).ToList();
                entityList = entityList.OrderBy(x => x.TableNumber).ToList();

                return new MessageResponse<List<Table>> { Entity = entityList };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Table>> { HasError = true, Message = $"Ocorreu um problema ao tentar buscar as informações de mesas com filtros. Ex: {ex.Message}" };
            }
        }

        public async Task<MessageResponse<Table>> UpdateStatusTableViaCaixa(Table request)
        {
            try
            {
                if (request != null)
                {
                    if (request.TableStatus == TableConstants.BLOCK_VALUE)
                    {
                        var entity = await _rTable.GetById(request.Id);
                        entity.ModifiedDate = DateTime.Now;
                        entity.TableStatus = request.TableStatus;
                        entity.ModifiedUserId = request.ModifiedUserId;

                        await _rTable.Update(entity);
                        return new MessageResponse<Table> { Entity = entity, HasError = false, Message = "Mesa atualizada com sucesso!" };
                    }

                    //Verificar se existe alguma conta aberta para a mesa.
                    AccountOrder accountsOrders = _rAccountOrder.GetAccountForTableSpecific(request.TableNumber).Result;

                    if (accountsOrders != null)
                        return new MessageResponse<Table> { Entity = request, HasError = true, Message = $"Existe uma conta atualmente com o status de {accountsOrders.StatusAccountOrder}. Você deseja continuar continuar com a alteração?" };
                    else
                    {
                        var entity = await _rTable.GetById(request.Id);
                        entity.ModifiedDate = DateTime.Now;
                        entity.TableStatus = request.TableStatus;
                        entity.ModifiedUserId = request.ModifiedUserId;

                        await _rTable.Update(entity);
                        return new MessageResponse<Table> { Entity = entity, HasError = false, Message = $"Mesa atualizada com sucesso!" };
                    }
                }
                else
                    return new MessageResponse<Table> { HasError = true, Message = "Não há nenhuma informação sobre a mesa. (null)" };
            }
            catch (Exception ex)
            {
                return new MessageResponse<Table> { HasError = true, Message = $"Não possível atualizar status da mesa - {ex.Message}" };
            }
        }

        public async Task<MessageResponse<Table>> ConfirmUpdateStatusTableViaCaixa(Table request)
        {
            try
            {
                if (request != null)
                {
                    var accountOrder = await _rAccountOrder.GetAccountForTableSpecific(request.TableNumber);
                    accountOrder.StatusAccountOrder = AccountOrderConstants.ACCOUNT_CLOSE_VALUE;
                    accountOrder.ModifiedDate = DateTime.Now;
                    accountOrder.ModifiedUserId = request.ModifiedUserId;

                    //Fecha a conta que estava com status aberta.
                    await _rAccountOrder.Update(accountOrder);

                    //Atualiza o status da mesa ( disponivel )
                    var entity = await _rTable.GetById(request.Id);
                    entity.ModifiedDate = DateTime.Now;
                    entity.TableStatus = request.TableStatus;
                    entity.ModifiedUserId = request.ModifiedUserId;
                    await _rTable.Update(entity);

                    return new MessageResponse<Table> { Entity = entity, Message = "Mesa atualizada com sucesso!" };
                }
                else
                {
                    return new MessageResponse<Table> { HasError = true, Message = "Não há nenhuma informação sobre a mesa. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Table> { HasError = true, Message = $"Não possível atualizar uma mesa - {ex.Message}" };
            }
        }

        public async Task<MessageResponse<Table>> Delete(int TableId)
        {
            try
            {
                if (TableId != 0)
                {
                    var table = await _rTable.GetById(TableId);
                    if (table != null)
                    {
                        if (table.TableStatus == TableConstants.BLOCK_VALUE)
                            return new MessageResponse<Table> { HasError = true, Message = "A mesa está ocupada, não é possível remover!" };

                        var account = await _rAccountOrder.GetAccountForTableSpecific(table.TableNumber);
                        if (account.StatusAccountOrder == AccountOrderConstants.ACCOUNT_OPEN_VALUE)
                            return new MessageResponse<Table> { HasError = true, Message = "A mesa está com uma conta aberta. Não é possível remover!" };

                        await _rTable.Delete(table);

                        return new MessageResponse<Table> { Entity = table, Message = "Mesa removida com sucesso!" };
                    }
                    else
                        return new MessageResponse<Table> { Entity = table, Message = "Mesa não encontrada para ser removida!" };
                }
                else
                {
                    return new MessageResponse<Table> { HasError = true, Message = $"Não há nenhuma mesa com o Id {TableId}!" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Table> { HasError = true, Message = $"Não foi possível excluir a mesa - {ex.Message}" };
            }

        }


    }
}
