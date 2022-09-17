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
        Task<List<Order>> GetListOrdersAsync();

        Task<bool> PostOrderAsync(OrderVM order);
    }
}
