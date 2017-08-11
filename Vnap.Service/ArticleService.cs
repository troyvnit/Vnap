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
        Task Sync();
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

        public async Task Sync()
        {
            var count = LocalDataStorage.GetArticles().AsQueryable().Count();
            if (count == 0)
            {
                FillContainer();
            }
        }

        private void FillContainer()
        {
            var startDate = new DateTime(2016, 1, 1);

            var posts = new List<ArticleEntity>();
            posts.Add(new ArticleEntity()
            {
                Id = 1,
                Priority = 2,
                Title = "Dự báo sâu bệnh tổng hợp trong tuần (22.08.2016)",
                Description = "Dự kiến tuần tới sẽ có đợt rầy nâu di trú rộ vì vậy khuyến cáo nông dân duy trì mực nước trên ruộng thích hợp để hạn chế rầy chích hút thân cây lúa đối với lúa Thu Đông dưới 25 ngày sau sạ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(1)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 2,
                Priority = 2,
                Title = "Xuất khẩu gạo tiếp tục bế tắc do thiếu thị trường",
                Description = "Theo thông tin từ Bộ Nông nghiệp và Phát triển Nông thôn, trong tháng Tám, xuất khẩu gạo tiếp tục gặp bế tắc do không có nhu cầu nhập khẩu gạo mới từ cả thị trường truyền thống và các thị trường khác.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(2)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 3,
                Priority = 3,
                Title = "Giá rau xanh tại Hà Nội không dao dộng nhiều trong ngày mưa bão",
                Description = "Mới vào khoảng 6 giờ sáng 19/8 nhưng quầy bán rau của chị Tuyền tại Khu vực Chợ Cầu Lủ, Định Công, Hoàng Mai, Hà Nội đã khá đông khách. ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 4,
                Priority = 4,
                Title = "Chi hàng chục triệu USD nhập đậu phộng Trung Quốc",
                Description = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 3,
                Priority = 3,
                Title = "Giá rau xanh tại Hà Nội không dao dộng nhiều trong ngày mưa bão",
                Description = "Mới vào khoảng 6 giờ sáng 19/8 nhưng quầy bán rau của chị Tuyền tại Khu vực Chợ Cầu Lủ, Định Công, Hoàng Mai, Hà Nội đã khá đông khách. ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 4,
                Priority = 4,
                Title = "Chi hàng chục triệu USD nhập đậu phộng Trung Quốc",
                Description = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 3,
                Priority = 3,
                Title = "Giá rau xanh tại Hà Nội không dao dộng nhiều trong ngày mưa bão",
                Description = "Mới vào khoảng 6 giờ sáng 19/8 nhưng quầy bán rau của chị Tuyền tại Khu vực Chợ Cầu Lủ, Định Công, Hoàng Mai, Hà Nội đã khá đông khách. ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            posts.Add(new ArticleEntity()
            {
                Id = 4,
                Priority = 4,
                Title = "Chi hàng chục triệu USD nhập đậu phộng Trung Quốc",
                Description = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });

            LocalDataStorage.SetArticles(posts);
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
