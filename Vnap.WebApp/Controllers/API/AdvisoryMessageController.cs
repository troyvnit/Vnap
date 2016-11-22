using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.Identity;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Models;

namespace Vnap.WebApp.Controllers.API
{
    [RoutePrefix("api/AdvisoryMessage")]
    public class AdvisoryMessageController : ApiController
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IAdvisoryMessageRepository _advisoryMessageRepository;

        public AdvisoryMessageController(IAdvisoryMessageRepository advisoryMessageRepository, IConversationRepository conversationRepository)
        {
            _advisoryMessageRepository = advisoryMessageRepository;
            _conversationRepository = conversationRepository;
        }

        // GET: api/AdvisoryMessage
        public async Task<IEnumerable<AdvisoryMessageVM>> GetAdvisoryMessages(string conversationName)
        {
            var query = await _advisoryMessageRepository.AllIncludingAsync(a => a.Conversation);
            IEnumerable<AdvisoryMessage> advisoryMessages = query.Where(a => a.Conversation.Name == conversationName);

            return Mapper.Map<IEnumerable<AdvisoryMessageVM>>(advisoryMessages);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<AdvisoryMessageVM> Get(int id)
        {
            var advisoryMessage = await _advisoryMessageRepository.GetSingleReadOnlyAsync(id);

            return Mapper.Map<AdvisoryMessageVM>(advisoryMessage);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<AdvisoryMessageVM> Add(AdvisoryMessageVM advisoryMessageVm)
        {
            var conversation = await _conversationRepository.Queryable().FirstOrDefaultAsync(c => c.Name == advisoryMessageVm.AuthorName);
            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    Name = advisoryMessageVm.AuthorName
                };
                _conversationRepository.Add(conversation);
            }
            var advisoryMessage = Mapper.Map<AdvisoryMessage>(advisoryMessageVm);
            conversation.AdvisoryMessages.Add(advisoryMessage);
            await _conversationRepository.CommitAsync();

            return Mapper.Map<AdvisoryMessageVM>(advisoryMessage); 
        }

        [HttpPost]
        [Route("Update")]
        public async Task<AdvisoryMessageVM> Update(AdvisoryMessageVM advisoryMessageVm)
        {
            var advisoryMessage = Mapper.Map<AdvisoryMessage>(advisoryMessageVm);
            _advisoryMessageRepository.Update(advisoryMessage);
            await _advisoryMessageRepository.CommitAsync();

            return advisoryMessageVm;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<AdvisoryMessageVM> Delete(AdvisoryMessageVM advisoryMessageVm)
        {
            await _advisoryMessageRepository.DeleteByIdAsync(advisoryMessageVm.Id);
            await _advisoryMessageRepository.CommitAsync();

            return advisoryMessageVm;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<AdvisoryMessageVM> Upload(string authorName)
        {
            try
            {
                Account account = new Account("vnap", "779243627828354", "83F-o-2dn-ZubPVpcS57SxsOabI");
                Cloudinary cloudinary = new Cloudinary(account);

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath($"~/App_Data/{postedFile?.FileName}");
                        postedFile?.SaveAs(filePath);

                        var imageUploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            UseFilename = true
                        };
                        var result = await cloudinary.UploadAsync(imageUploadParams);
                        var conversation = await _conversationRepository.Queryable().FirstOrDefaultAsync(c => c.Name == authorName);
                        if (conversation == null)
                        {
                            conversation = new Conversation()
                            {
                                Name = authorName
                            };
                            _conversationRepository.Add(conversation);
                        }
                        var advisoryMessage = new AdvisoryMessage()
                        {
                            AuthorName = authorName,
                            ImageUrl = result.Uri.AbsoluteUri
                        };
                        conversation.AdvisoryMessages.Add(advisoryMessage);
                        await _conversationRepository.CommitAsync();
                        return Mapper.Map<AdvisoryMessageVM>(advisoryMessage);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}