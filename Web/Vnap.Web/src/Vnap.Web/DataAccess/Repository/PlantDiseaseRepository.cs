using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface IPlantDiseaseRepository : IRepository<PlantDisease>
    { }

    public class PlantDiseaseRepository : RepositoryBase<PlantDisease>, IPlantDiseaseRepository
    {
        public PlantDiseaseRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
