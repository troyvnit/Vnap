using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Vnap.Web.Data;
using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private ApplicationDbContext _context;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ApplicationDbContext DbContext
        {
            get { return _context ?? (_context = DbFactory.Init()); }
        }

        #region Properties
        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _context = DbContext;
        }
        #endregion
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public virtual IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> AllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }
        public TEntity GetSingle(int id)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public async Task<TEntity> GetSingleAsync(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<TEntity> GetSingleReadOnlyAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> Queryable()
        {
            return DbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Add(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            TEntity entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void Detach(TEntity entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Detached;
        }
    }
}
