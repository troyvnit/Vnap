using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface IImageRepository : IRepository<Image>
    { }

    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
