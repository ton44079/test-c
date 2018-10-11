using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestC.Repositories.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        bool All(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    }
}
