using Domain.Interface;
using Entities.Entities;
using Infra.Context;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ROrder : RestorantiRepository<Order>, IROrder
    {
        private readonly DbContextOptions<RestorantiContext> _optionsBuilder;
        public ROrder()
        {
            this._optionsBuilder = new DbContextOptions<RestorantiContext>();
        }

        public async Task<List<Order>> GetByOrderNumber(string OrderNumber)
        {
            using (var data = new RestorantiContext(_optionsBuilder))
            {
                var result = await data.Set<Order>().Where(x => x.OrderNumber == OrderNumber).ToListAsync();
                if (result != null)
                    return result;

                return null;
            }
        }
    }
}
