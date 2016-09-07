using System;
using System.Threading;
using System.Threading.Tasks;
using Vnap.Core.DataAccess.Repository;

namespace Vnap.Core.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();
        TRepository GetCustomRepository<TRepository>();
    }
}
