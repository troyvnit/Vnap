using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface IPlantRepository : IRepository<Plant>
    { }

    public class PlantRepository : RepositoryBase<Plant>, IPlantRepository
    {
        public PlantRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
