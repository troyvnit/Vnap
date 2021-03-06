﻿using System;
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
        List<AdvisoryMessageEntity> GetMessages(GetMessagesRq rq);
        int GetMessagesCount();
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

        public List<AdvisoryMessageEntity> GetMessages(GetMessagesRq rq)
        {
            var query = LocalDataStorage.GetAdvisoryMessages()
                .OrderBy(message => message.CreatedDate)
                .AsQueryable();
            return query.ToList();
        }

        public int GetMessagesCount()
        {
            return LocalDataStorage.GetAdvisoryMessages().AsQueryable().Count();
        }
    }
}
