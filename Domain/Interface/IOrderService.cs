using Entities.Entities;
using Entities.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IOrderService
    {
        Task<MessageResponse<List<OrderResponseVM>>> GetListOrdersAsync();
        Task<MessageResponse<List<Order>>> PostOrderAsync(OrderVM order);
    }
}
