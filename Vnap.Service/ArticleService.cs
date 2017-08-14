using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Article;
using Vnap.Service.Utils;

namespace Vnap.Service
{
    public interface IArticleService
    {
        List<ArticleEntity> GetArticles(GetArticlesRq rq);
        int GetArticlesCount();
    }
    public class ArticleService : IArticleService
    {
        private IRepository<ArticleEntity> _postRepository;
        public ArticleService(IRepository<ArticleEntity> postRepository)
        {
            _postRepository = postRepository;
        }

        public List<ArticleEntity> GetArticles(GetArticlesRq rq)
        {
            var query = LocalDataStorage.GetArticles()
                .OrderByDescending(post => post.Priority)
                .ThenByDescending(post => post.CreatedDate)
                .AsQueryable();
            query = query.Where(post => post.ArticleType == rq.ArticleType);
            query = query.Skip(rq.Skip).Take(rq.Take);
            return query.ToList();
        }

        public ArticleEntity SearchFirstArticle(string query)
        {
            return LocalDataStorage.GetArticles().AsQueryable().FirstOrDefault(post => post.Title.Contains(query) || post.Description.Contains(query));
        }

        public int GetArticlesCount()
        {
            return LocalDataStorage.GetArticles().AsQueryable().Count();
        }
    }
}
