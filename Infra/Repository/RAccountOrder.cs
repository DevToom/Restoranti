using Domain.Interface;
using Entities.Entities;
using Entities.Entities.Constants;
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
    public class RAccountOrder : RestorantiRepository<AccountOrder>, IRAccountOrder
    {
        private readonly DbContextOptions<RestorantiContext> _optionsBuilder;
        public RAccountOrder()
        {
            this._optionsBuilder = new DbContextOptions<RestorantiContext>();
        }

        public async Task<List<AccountOrder>> ValidateIfTableIsAvailable(int TableNumber)
        {
            using (var data = new RestorantiContext(_optionsBuilder))
            {
                var result = await data.Set<AccountOrder>().Where(x => x.TableNumber == TableNumber && x.StatusAccountOrder == AccountOrderConstants.ACCOUNT_OPEN_VALUE).ToListAsync();
                if (result != null)
                    return result;

                return null;
            }
        }

    }
}
