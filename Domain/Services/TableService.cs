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

        public TableService(IRTable rTable)
        {
            this._rTable = rTable;
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

        public async Task<MessageResponse<Table>> Delete(int TableId)
        {
            try
            {
                var table = _rTable.GetById(TableId).Result;
                if (table != null)
                {
                    await _rTable.Delete(table);
                    return new MessageResponse<Table> { Entity = null, Message = "Mesa removido com sucesso!" };
                }
                else
                {
                    return new MessageResponse<Table> { HasError = true, Message = "Não foi encontrada nenhuma mesa com esse ID. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Table> { HasError = true, Message = $"Não possível remover a mesa - {ex.Message}" };
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
    }
}
