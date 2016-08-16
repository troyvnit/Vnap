using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using Vnap.Entity;
using Xamarin.Forms;

namespace Vnap.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        AsyncTableQuery<T> AsQueryable();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task CreateTableAsync();
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly SQLiteAsyncConnection _db;

        public Repository()
        {
            _db = DependencyService.Get<ISQLiteProvider>().GetSQLiteAsyncConnection("Vnap.db3");
        }

        public AsyncTableQuery<T> AsQueryable() =>
            _db.Table<T>();

        public Task<List<T>> Get() =>
            _db.Table<T>().ToListAsync();

        public Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return query.ToListAsync();
        }

        public Task<T> Get(int id) =>
             _db.FindAsync<T>(id);

        public Task<T> Get(Expression<Func<T, bool>> predicate) =>
            _db.FindAsync<T>(predicate);

        public Task<int> Insert(T entity) =>
             _db.InsertAsync(entity);

        public Task<int> Update(T entity) =>
             _db.UpdateAsync(entity);

        public Task<int> Delete(T entity) =>
             _db.DeleteAsync(entity);

        public Task CreateTableAsync() =>
            _db.CreateTableAsync<T>();
    }
}
