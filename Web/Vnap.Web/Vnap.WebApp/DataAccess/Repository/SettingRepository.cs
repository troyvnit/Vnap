using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface ISettingRepository : IRepository<Setting>
    { }

    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        public SettingRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
