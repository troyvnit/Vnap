using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface IArticleRepository : IRepository<Article>
    { }

    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
