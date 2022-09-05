using Entities.Entities;
using Infra.Repository.Generics.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Interface
{
    public interface IRUserInternal : IRestorantiGeneric<UserInternal>
    {
        Task<bool> CreateAsync(UserInternal user);
        Task<UserInternal> GetByUsername(UserInternal user);
    }
}
