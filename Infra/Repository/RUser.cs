using Entities.Entities;
using Infra.Context;
using Infra.Repository.Generics;
using Infra.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class RUserInternal : RestorantiRepository<UserInternal>, IRUserInternal
    {
        private readonly DbContextOptions<RestorantiContext> _optionsBuilder;
        public RUserInternal()
        {
            this._optionsBuilder = new DbContextOptions<RestorantiContext>();
        }

        public async Task<bool> CreateAsync(UserInternal user)
        {
            using (var data = new RestorantiContext(_optionsBuilder))
            {
                //await data.Set<User>().AddAsync(user);

                var result = data.AddAsync(user);
                       await data.SaveChangesAsync();

                if (result.IsCompleted && result.IsCompletedSuccessfully)
                    return true;
                else
                    return false;
            }
        }
    }
}
