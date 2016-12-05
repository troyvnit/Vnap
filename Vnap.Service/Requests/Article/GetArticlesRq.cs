using Vnap.Entity;

namespace Vnap.Service.Requests.Article
{
    public class GetArticlesRq : BaseRq
    {
        public ArticleType ArticleType { get; set; }
    }
}
