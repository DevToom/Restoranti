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
    public class AccountOrderService : IAccountOrderService
    {
        private readonly IRAccountOrder _rAccountOrder;

        public AccountOrderService(IRAccountOrder rAccountOrder)
        {
            this._rAccountOrder = rAccountOrder;
        }

        public async Task<MessageResponse<AccountOrder>> Add(AccountOrder account)
        {
            try
            {
                if (account != null)
                {
                    var table = await _rAccountOrder.ValidateIfTableIsAvailable(account.TableNumber);

                    await _rAccountOrder.AddAsync(account);
                    return new MessageResponse<AccountOrder> { Entity = account, Message = "Produto adicionada com sucesso!" };
                }
                else
                {
                    return new MessageResponse<AccountOrder> { HasError = true, Message = "Não há nenhuma informação sobre o novo produto. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<AccountOrder> { HasError = true, Message = $"Não foi possível adicionar  o novo produto - {ex.Message}" };
            }

        }

        public async Task<MessageResponse<AccountOrder>> OpenAccount(int TableNumber, int UserId)
        {
            try
            {
                if (TableNumber > 0)
                {
                    var accountTable = await _rAccountOrder.ValidateIfTableIsAvailable(TableNumber);
                    if (accountTable.Any())
                        return new MessageResponse<AccountOrder> { HasError = true, Message = "Já existe uma conta aberta para a mesa!" };

                    AccountOrder accountOrder = new AccountOrder();
                    accountOrder.StatusAccountOrder = AccountOrderConstants.ACCOUNT_OPEN_VALUE;
                    accountOrder.UserAppId = UserId;
                    accountOrder.TableNumber = TableNumber;

                    await _rAccountOrder.AddAsync(accountOrder);
                    return new MessageResponse<AccountOrder> { Entity = accountOrder, Message = "Conta aberta com sucesso!" };
                }
                else
                {
                    return new MessageResponse<AccountOrder> { HasError = true, Message = "Não existe nenhuma mesa com o número 0. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<AccountOrder> { HasError = true, Message = $"Não foi possível abrir a conta para a mesa {TableNumber} - {ex.Message}" };
            }

        }



    }
}
