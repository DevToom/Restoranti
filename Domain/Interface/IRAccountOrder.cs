using Entities.Entities;
using Infra.Repository.Generics.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRAccountOrder : IRestorantiGeneric<AccountOrder>
    {
        Task<List<AccountOrder>> ValidateIfTableIsAvailable(int TableNumber);
    }
}
