using Infra.Context;
using Infra.Repository.Generics.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Generics
{
    public class RestorantiRepository<T> : IRestorantiRepository<T>, IDisposable where T : class
    {
        private readonly RestorantiContext _context;
        private readonly DbSet<T> _table;

        public RestorantiRepository(RestorantiContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
        public void Add(T entity)
        {
            if (entity != null)
            {
                var a = _table.Add(entity);
                Save();
                Dispose();
            }
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
