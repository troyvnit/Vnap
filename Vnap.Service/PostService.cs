using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Post;
using Vnap.Service.Utils;

namespace Vnap.Service
{
    public interface IPostService
    {
        Task Sync();
        Task<List<PostEntity>> GetPosts(GetPostsRq rq);
        Task<int> GetPostsCount();
    }
    public class PostService : IPostService
    {
        private IRepository<PostEntity> _postRepository;
        public PostService(IRepository<PostEntity> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Sync()
        {
            var count = LocalDataStorage.GetPosts().AsQueryable().Count();
            if (count == 0)
            {
                FillContainer();
            }
        }

        private void FillContainer()
        {
            var startDate = new DateTime(2016, 1, 1);

            var posts = new List<PostEntity>();
            posts.Add(new PostEntity()
            {
                Id = 1,
                Priority = 2,
                Title = "Dự báo sâu bệnh tổng hợp trong tuần (22.08.2016)",
                Description = "Dự kiến tuần tới sẽ có đợt rầy nâu di trú rộ vì vậy khuyến cáo nông dân duy trì mực nước trên ruộng thích hợp để hạn chế rầy chích hút thân cây lúa đối với lúa Thu Đông dưới 25 ngày sau sạ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(1)
            });
            posts.Add(new PostEntity()
            {
                Id = 2,
                Priority = 2,
                Title = "Xuất khẩu gạo tiếp tục bế tắc do thiếu thị trường",
                Description = "Theo thông tin từ Bộ Nông nghiệp và Phát triển Nông thôn, trong tháng Tám, xuất khẩu gạo tiếp tục gặp bế tắc do không có nhu cầu nhập khẩu gạo mới từ cả thị trường truyền thống và các thị trường khác.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(2)
            });
            posts.Add(new PostEntity()
            {
                Id = 3,
                Priority = 3,
                Title = "Giá rau xanh tại Hà Nội không dao dộng nhiều trong ngày mưa bão",
                Description = "Mới vào khoảng 6 giờ sáng 19/8 nhưng quầy bán rau của chị Tuyền tại Khu vực Chợ Cầu Lủ, Định Công, Hoàng Mai, Hà Nội đã khá đông khách. ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            posts.Add(new PostEntity()
            {
                Id = 4,
                Priority = 4,
                Title = "Chi hàng chục triệu USD nhập đậu phộng Trung Quốc",
                Description = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });
            posts.Add(new PostEntity()
            {
                Id = 3,
                Priority = 3,
                Title = "Giá rau xanh tại Hà Nội không dao dộng nhiều trong ngày mưa bão",
                Description = "Mới vào khoảng 6 giờ sáng 19/8 nhưng quầy bán rau của chị Tuyền tại Khu vực Chợ Cầu Lủ, Định Công, Hoàng Mai, Hà Nội đã khá đông khách. ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            posts.Add(new PostEntity()
            {
                Id = 4,
                Priority = 4,
                Title = "Chi hàng chục triệu USD nhập đậu phộng Trung Quốc",
                Description = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });
            posts.Add(new PostEntity()
            {
                Id = 3,
                Priority = 3,
                Title = "Giá rau xanh tại Hà Nội không dao dộng nhiều trong ngày mưa bão",
                Description = "Mới vào khoảng 6 giờ sáng 19/8 nhưng quầy bán rau của chị Tuyền tại Khu vực Chợ Cầu Lủ, Định Công, Hoàng Mai, Hà Nội đã khá đông khách. ",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            posts.Add(new PostEntity()
            {
                Id = 4,
                Priority = 4,
                Title = "Chi hàng chục triệu USD nhập đậu phộng Trung Quốc",
                Description = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });

            LocalDataStorage.SetPosts(posts);
        }

        public async Task<List<PostEntity>> GetPosts(GetPostsRq rq)
        {
            var query = LocalDataStorage.GetPosts()
                .OrderByDescending(post => post.Priority)
                .ThenByDescending(post => post.CreatedDate)
                .AsQueryable();
            if (rq.PostType != PostType.All)
            {
               query = query.Where(post => post.PostType == rq.PostType);
            }
            query = query.Skip(rq.Skip).Take(rq.Take);
            return query.ToList();
        }

        public async Task<PostEntity> SearchFirstPost(string query)
        {
            return LocalDataStorage.GetPosts().AsQueryable().FirstOrDefault(post => post.Title.Contains(query) || post.Description.Contains(query));
        }

        public async Task<int> GetPostsCount()
        {
            return LocalDataStorage.GetPosts().AsQueryable().Count();
        }
    }
}
