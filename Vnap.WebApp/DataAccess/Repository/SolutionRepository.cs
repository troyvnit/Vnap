using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface ISolutionRepository : IRepository<Solution>
    { }

    public class SolutionRepository : RepositoryBase<Solution>, ISolutionRepository
    {
        public SolutionRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
