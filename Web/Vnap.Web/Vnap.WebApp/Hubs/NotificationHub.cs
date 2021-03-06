﻿using AutoMapper;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Utilities;

namespace Vnap.WebApp.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IConversationRepository _conversationRepository;

        public NotificationHub(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task HandShake(string conversationName)
        {
            var conversation = await _conversationRepository.Queryable().FirstOrDefaultAsync(c => c.Name == conversationName);
            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    Name = conversationName,
                    ConnectionId = Context.ConnectionId
                };
                _conversationRepository.Add(conversation);
            }
            else
            {
                conversation.ConnectionId = Context.ConnectionId;
                _conversationRepository.Update(conversation);
            }
            await _conversationRepository.CommitAsync();
        }

        public async Task<AdvisoryMessageVM> SubscribeAdvisoryMessage(AdvisoryMessageVM advisoryMessageVm)
        {
            var conversationName = advisoryMessageVm.ConversationName ?? advisoryMessageVm.AuthorName;
            var conversation = await _conversationRepository.Queryable().FirstOrDefaultAsync(c => c.Name == conversationName);
            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    Name = conversationName
                };
                _conversationRepository.Add(conversation);
            }
            var advisoryMessage = Mapper.Map<AdvisoryMessage>(advisoryMessageVm);
            conversation.AdvisoryMessages.Add(advisoryMessage);
            await _conversationRepository.CommitAsync();

            advisoryMessageVm = Mapper.Map<AdvisoryMessageVM>(advisoryMessage);

            await PublishAdvisoryMessage(advisoryMessageVm);

            return advisoryMessageVm;
        }

        public async Task PublishAdvisoryMessage(AdvisoryMessageVM advisoryMessageVm)
        {
            if (advisoryMessageVm.IsAdviser)
            {
                var conversation = await _conversationRepository.Queryable().FirstOrDefaultAsync(c => c.Name == advisoryMessageVm.ConversationName);
                if (conversation == null)
                {
                    Clients.All.PublishAdvisoryMessage(advisoryMessageVm);
                }
                else
                {
                    Clients.Client(conversation.ConnectionId).PublishAdvisoryMessage(advisoryMessageVm);
                }
                
                GcmUtil.SendGcmMessage(JsonConvert.SerializeObject(advisoryMessageVm), "advisory");
            }
            else
            {
                Clients.All.PublishAdvisoryMessage(advisoryMessageVm);
            }
        }
    }
}