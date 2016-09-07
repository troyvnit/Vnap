using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Vnap.Core.DataAccess.Repository
{
    public abstract class BaseRepository<TContext> : IRepositoryInjection<TContext> where TContext : DbContext
    {
        protected BaseRepository(ILogger logger, TContext context)
        {
            this.Logger = logger;
            this.Context = context;
        }

        protected ILogger Logger { get; private set; }
        protected TContext Context { get; private set; }

        IRepositoryInjection<TContext> IRepositoryInjection<TContext>.SetContext(TContext context)
        {
            this.Context = context;
            return this;
        }
    }
}
