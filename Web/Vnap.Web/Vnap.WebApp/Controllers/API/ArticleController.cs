using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Models;
using Vnap.WebApp.Hubs;
using Microsoft.AspNet.SignalR;

namespace Vnap.WebApp.Controllers.API
{
    [RoutePrefix("api/Article")]
    public class ArticleController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        // GET: api/Article
        public async Task<IEnumerable<ArticleVM>> GetArticles(int skip = 0, int take = 10)
        {
            IEnumerable<Article> pageData = await _articleRepository.GetAllAsync();

            return Mapper.Map<IEnumerable<ArticleVM>>(pageData);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ArticleVM> Get(int id)
        {
            var article = await _articleRepository.GetSingleReadOnlyAsync(id);

            return Mapper.Map<ArticleVM>(article);
        }

        [HttpGet]
        [Route("GetByLatestId")]
        public async Task<IEnumerable<ArticleVM>> GetArticlesByLatestId(int latestId)
        {
            var query = await _articleRepository.AllIncludingAsync();
            IEnumerable<Article> articles = query.Where(a => a.Id > latestId);

            return Mapper.Map<IEnumerable<ArticleVM>>(articles);
        }

        [HttpGet]
        [Route("GetByTypeAndLatestId")]
        public async Task<IEnumerable<ArticleVM>> GetArticlesByTypeAndLatestId(ArticleType articleType, int latestId)
        {
            var query = await _articleRepository.AllIncludingAsync();
            IEnumerable<Article> articles = query.Where(a => a.ArticleType == articleType && a.Id > latestId);

            return Mapper.Map<IEnumerable<ArticleVM>>(articles);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IEnumerable<ArticleVM>> Search(string query)
        {
            var articles = await _articleRepository.FindByAsync(p => p.Title.Contains(query));

            return Mapper.Map<IEnumerable<ArticleVM>>(articles);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ArticleVM> Add(ArticleVM articleVm)
        {
            var article = Mapper.Map<Article>(articleVm);
            _articleRepository.Add(article);
            await _articleRepository.CommitAsync();
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.All.PublishArticle(articleVm);
            return articleVm;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ArticleVM> Update(ArticleVM articleVm)
        {
            var article = Mapper.Map<Article>(articleVm);
            _articleRepository.Update(article);
            await _articleRepository.CommitAsync();

            return articleVm;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ArticleVM> Delete(ArticleVM articleVm)
        {
            await _articleRepository.DeleteByIdAsync(articleVm.Id);
            await _articleRepository.CommitAsync();

            return articleVm;
        }
    }
}