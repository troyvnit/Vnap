using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleAsync(int id);
        Task<T> GetSingleReadOnlyAsync(int id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> Queryable();
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        Task DeleteByIdAsync(int id);
        void Update(T entity);
        void Commit();
        Task CommitAsync();
        void Detach(T entity);
    }
}
