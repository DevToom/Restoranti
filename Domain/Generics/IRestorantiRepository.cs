using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Generics.Interface
{
    public interface IRestorantiGeneric<T> where T : class
    {
        Task AddAsync(T Entity);
        Task Update(T Entity);
        Task Delete(T Entity);
        Task<T> GetById(int Id);
        Task<List<T>> GetList();
    }
}
