using Entities.Entities;
using Entities.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IAccountOrderService
    {
        Task<MessageResponse<AccountOrder>> Add(AccountOrder product);
        Task<MessageResponse<AccountOrder>> OpenAccount(int TableNumber, int UserId);
        Task<MessageResponse<AccountOrder>> UpdateValueTotalAccount(OrderVM request);

    }
}
