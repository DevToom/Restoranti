using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Generics.Interface
{
    public interface IRestorantiRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetList(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
        void Dispose();
    }
}
