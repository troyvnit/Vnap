using Microsoft.EntityFrameworkCore;

namespace Vnap.Core.DataAccess.Repository
{
    public interface IRepositoryInjection<TContext> where TContext : DbContext
    {
        IRepositoryInjection<TContext> SetContext(TContext context);
    }
}