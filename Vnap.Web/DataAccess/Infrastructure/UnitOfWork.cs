using Vnap.Web.Data;

namespace Vnap.Web.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private ApplicationDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public ApplicationDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public int Commit()
        {
           return DbContext.Commit();
        }
    }
}
