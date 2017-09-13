using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Newtonsoft.Json;
using Vnap.Entity;
using Vnap.Models;
using Vnap.Service.Utils;
using Xamarin.Forms;
using Microsoft.AspNet.SignalR.Client;

namespace Vnap.Droid.Services.BackgroundServices
{
    [Service]
    public class NotificationBackgroundService : Android.App.Service
    {
        private CancellationTokenSource _cts;
        private NotificationService _notificationService = new NotificationService();
        HttpClient httpClient = new HttpClient();

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            try
            {
                if (App.HubConnection.State == ConnectionState.Disconnected)
                {
                    App.HubConnection.Start().Wait();
                    App.HubProxy.Invoke("HandShake", App.CurrentUser.UserName).Wait();
                }

                App.HubProxy.Subscribe("PublishAdvisoryMessage").Received += rs => {
                    var newMessage = rs[0]?.ToObject<AdvisoryMessage>();
                    if (newMessage?.ConversationName == App.CurrentUser.UserName && newMessage.IsAdviser)
                    {
                        _notificationService.Notify("Vnap đã trả lời câu hỏi của bạn!", !string.IsNullOrEmpty(newMessage.Content) ? newMessage.Content : "[Hình ảnh]", 1);
                        MessagingCenter.Send(newMessage, "UpdateAdvisoryMessages");
                    }
                };

                App.HubProxy.Subscribe("PublishArticle").Received += rs => {
                    var newArticle = rs[0]?.ToObject<Article>();
                    if (newArticle != null && (newArticle.ArticleType == ArticleType.News || newArticle.ArticleType == ArticleType.Notice))
                    {
                        if (newArticle.IsActived)
                        {
                            _notificationService.Notify(newArticle.Title, !string.IsNullOrEmpty(newArticle.Description) ? newArticle.Description : newArticle.Content, 1);
                        }
                        MessagingCenter.Send(newArticle, "UpdateArticles"); 
                    }
                };
            }
            catch (Exception e)
            {
            }

            //_cts = new CancellationTokenSource();

            //Task.Run(async () =>
            //{
            //    try
            //    {
            //        while (!_cts.IsCancellationRequested)
            //        {
            //            if(App.CurrentUser != null && !string.IsNullOrEmpty(App.CurrentUser.UserName))
            //            {
            //                var advisoryMessages = LocalDataStorage.GetAdvisoryMessages();
            //                var isNotEmpty = advisoryMessages.Any();
            //                var url = isNotEmpty
            //                        ? $"http://vnap.vn/api/AdvisoryMessage/GetByLatestId?conversationName={App.CurrentUser.UserName}&latestId={advisoryMessages.OrderByDescending(a => a.CreatedDate).FirstOrDefault()?.Id}"
            //                        : $"http://vnap.vn/api/AdvisoryMessage?conversationName={App.CurrentUser.UserName}";
            //                var getAdvisoryMessagesRs = await httpClient.GetStringAsync(url);
            //                var newAdvisoryMessages = JsonConvert.DeserializeObject<List<AdvisoryMessageEntity>>(getAdvisoryMessagesRs);

            //                if (newAdvisoryMessages.Any())
            //                {
            //                    advisoryMessages.AddRange(newAdvisoryMessages.ToList());
            //                    LocalDataStorage.SetAdvisoryMessages(advisoryMessages);
            //                    var first = newAdvisoryMessages.OrderByDescending(a => a.CreatedDate).FirstOrDefault(a => a.IsAdviser);
            //                    if (first != null && isNotEmpty)
            //                    {
            //                        _notificationService.Notify("Vnap đã trả lời câu hỏi của bạn!", !string.IsNullOrEmpty(first.Content) ? first.Content : "[Hình ảnh]", newAdvisoryMessages.Count);
            //                    }
            //                }
            //            }
            //            Log.Info("", "Reconnecting to stream in 10 seconds");
            //            Thread.Sleep(10000);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        // Suppress?
            //    }
            //    finally
            //    {
            //        if (_cts.IsCancellationRequested)
            //        {
            //            var message = new CancelledMessage();
            //            Device.BeginInvokeOnMainThread(
            //                () => MessagingCenter.Send(message, "CancelledMessage")
            //                );
            //        }
            //    }
            //}, _cts.Token);

            //Task.Run(async () =>
            //{
            //    try
            //    {
            //        while (!_cts.IsCancellationRequested)
            //        {
            //            var articles = LocalDataStorage.GetArticles();
            //            var isNotEmpty = articles.Any();
            //            var url = isNotEmpty
            //                    ? $"http://vnap.vn/api/Article/GetByLatestId?latestId={articles.Max(x => x.Id)}"
            //                    : $"http://vnap.vn/api/Article";
            //            var getArticlesRs = await httpClient.GetStringAsync(url);
            //            var newArticles = JsonConvert.DeserializeObject<List<ArticleEntity>>(getArticlesRs);

            //            if (newArticles.Any())
            //            {
            //                articles.AddRange(newArticles.ToList());
            //                LocalDataStorage.SetArticles(articles);
            //                var first = newArticles.OrderByDescending(a => a.CreatedDate).FirstOrDefault();
            //                if (first != null && isNotEmpty)
            //                {
            //                    _notificationService.Notify(first.Title, !string.IsNullOrEmpty(first.Description) ? first.Description : first.Content, newArticles.Count);
            //                }
            //            }
            //            Log.Info("", "Reconnecting to stream in 10 seconds");
            //            Thread.Sleep(10000);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        // Suppress?
            //    }
            //    finally
            //    {
            //        if (_cts.IsCancellationRequested)
            //        {
            //            var message = new CancelledMessage();
            //            Device.BeginInvokeOnMainThread(
            //                () => MessagingCenter.Send(message, "CancelledMessage")
            //                );
            //        }
            //    }
            //}, _cts.Token);

            return StartCommandResult.Sticky;
        }
    }
}