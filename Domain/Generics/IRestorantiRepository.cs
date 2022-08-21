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
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetById(int Id);
        Task<List<T>> GetList();
    }
}
