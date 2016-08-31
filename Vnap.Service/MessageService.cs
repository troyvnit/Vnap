using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Message;
using Vnap.Service.Requests.Plant;

namespace Vnap.Service
{
    public interface IMessageService
    {
        Task Sync();
        Task<List<AdvisoryMessageEntity>> GetMessages(GetMessagesRq rq);
        Task<int> GetMessagesCount();
    }
    public class MessageService : IMessageService
    {
        private IRepository<AdvisoryMessageEntity> _messageRepository;
        public MessageService(IRepository<AdvisoryMessageEntity> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Sync()
        {
            var count = await _messageRepository.AsQueryable().CountAsync();
            if (count == 0)
            {
                FillContainer();
            }
        }

        private void FillContainer()
        {
            var startDate = new DateTime(2016, 1, 1);

            _messageRepository.Insert(new AdvisoryMessageEntity()
            {
                Id = 1,
                Content = "Dự báo sâu bệnh tổng hợp trong tuần (22.08.2016)",
                AuthorName = "Troy Lee",
                IsAdviser = true,
                CreatedDate = startDate.AddDays(1)
            });
            _messageRepository.Insert(new AdvisoryMessageEntity()
            {
                Id = 2,
                Content = "Theo thông tin từ Bộ Nông nghiệp và Phát triển Nông thôn, trong tháng Tám, xuất khẩu gạo tiếp tục gặp bế tắc do không có nhu cầu nhập khẩu gạo mới từ cả thị trường truyền thống và các thị trường khác.",
                CreatedDate = startDate.AddDays(2)
            });
            _messageRepository.Insert(new AdvisoryMessageEntity()
            {
                Id = 3,
                Content = "Dự báo sâu bệnh tổng hợp trong tuần (22.08.2016)",
                ImageUrl = "http://chuyennongvu.vn/wp-content/uploads/2015/06/da3lua.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            _messageRepository.Insert(new AdvisoryMessageEntity()
            {
                Id = 4,
                AuthorName = "Troy Lee",
                IsAdviser = true,
                Content = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                CreatedDate = startDate.AddDays(4)
            });
        }

        public async Task<List<AdvisoryMessageEntity>> GetMessages(GetMessagesRq rq)
        {
            var query = _messageRepository.AsQueryable()
                .OrderByDescending(message => message.CreatedDate);
            query = query.Skip(rq.Skip).Take(rq.Take);
            return await query.ToListAsync();
        }

        public async Task<int> GetMessagesCount()
        {
            return await _messageRepository.AsQueryable().CountAsync();
        }
    }
}
