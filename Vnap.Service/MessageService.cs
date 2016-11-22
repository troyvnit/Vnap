using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Message;
using Vnap.Service.Requests.Plant;
using Vnap.Service.Utils;

namespace Vnap.Service
{
    public interface IMessageService
    {
        Task Sync(string currentUserName);
        Task<List<AdvisoryMessageEntity>> GetMessages(GetMessagesRq rq);
        Task<int> GetMessagesCount();
    }

    public class MessageService : IMessageService
    {
        HttpClient httpClient = new HttpClient();
        private IRepository<AdvisoryMessageEntity> _messageRepository;

        public MessageService(IRepository<AdvisoryMessageEntity> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Sync(string currentUserName)
        {
            try
            {
                var getAdvisoryMessagesRs = await httpClient.GetStringAsync($"http://vnap.vn/api/advisorymessage?conversationName={currentUserName}");
                var advisoryMessages = JsonConvert.DeserializeObject<List<AdvisoryMessageEntity>>(getAdvisoryMessagesRs);
                LocalDataStorage.SetAdvisoryMessages(advisoryMessages);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void FillContainer()
        {
            var startDate = new DateTime(2016, 1, 1);

            var messages = new List<AdvisoryMessageEntity>();
            messages.Add(new AdvisoryMessageEntity()
            {
                Id = 1,
                Content = "Dự báo sâu bệnh tổng hợp trong tuần (22.08.2016)",
                AuthorName = "Troy Lee",
                IsAdviser = true,
                CreatedDate = startDate.AddDays(1)
            });
            messages.Add(new AdvisoryMessageEntity()
            {
                Id = 2,
                Content = "Theo thông tin từ Bộ Nông nghiệp và Phát triển Nông thôn, trong tháng Tám, xuất khẩu gạo tiếp tục gặp bế tắc do không có nhu cầu nhập khẩu gạo mới từ cả thị trường truyền thống và các thị trường khác.",
                CreatedDate = startDate.AddDays(2)
            });
            messages.Add(new AdvisoryMessageEntity()
            {
                Id = 3,
                Content = "Dự báo sâu bệnh tổng hợp trong tuần (22.08.2016)",
                ImageUrl = "http://chuyennongvu.vn/wp-content/uploads/2015/06/da3lua.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            messages.Add(new AdvisoryMessageEntity()
            {
                Id = 4,
                AuthorName = "Troy Lee",
                IsAdviser = true,
                Content = "Sau bắp, đậu nành... những năm gần đây Việt Nam lại đua nhau nhập đậu phộng, trong đó nguồn đậu phộng nhập khẩu chủ yếu từ Trung Quốc.",
                CreatedDate = startDate.AddDays(4)
            });

            LocalDataStorage.SetAdvisoryMessages(messages);
        }

        public async Task<List<AdvisoryMessageEntity>> GetMessages(GetMessagesRq rq)
        {
            await Sync(rq.ConversationName);
            var query = LocalDataStorage.GetAdvisoryMessages()
                .OrderByDescending(message => message.CreatedDate)
                .AsQueryable();
            query = query.Skip(rq.Skip).Take(rq.Take);
            return query.ToList();
        }

        public async Task<int> GetMessagesCount()
        {
            return LocalDataStorage.GetAdvisoryMessages().AsQueryable().Count();
        }
    }
}
