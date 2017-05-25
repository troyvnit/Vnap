using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface IAdvisoryMessageRepository : IRepository<AdvisoryMessage>
    { }

    public class AdvisoryMessageRepository : RepositoryBase<AdvisoryMessage>, IAdvisoryMessageRepository
    {
        public AdvisoryMessageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
