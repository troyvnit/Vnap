using System;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AutoMapper;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Messaging;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Message;
using Vnap.Service.Requests.Plant;
using Xamarin.Forms;
using Image = Vnap.Models.Image;
using Vnap.Service.Utils;
using Microsoft.AspNet.SignalR.Client;
using Vnap.Entity;
using Vnap.Services;
using Vnap.Views.ExtendedControls;

namespace Vnap.ViewModels
{
    public class AdvisoryTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly HttpClient _httpClient = new HttpClient();
        private bool subscribed;

        private ObservableRangeCollection<AdvisoryMessage> _messages = new ObservableRangeCollection<AdvisoryMessage>();
        public ObservableRangeCollection<AdvisoryMessage> Messages => _messages;

        private string _newMessage = null;
        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                SetProperty(ref _newMessage, value);
            }
        }

        private int _totalMessages;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<AdvisoryMessage> LoadMoreCommand { get; set; }
        public DelegateCommand<AdvisoryMessage> ItemClickCommand { get; set; }
        public DelegateCommand SendAdvisoryMessageCommand { get; set; }
        public DelegateCommand TakeOrPickPhotoCommand { get; set; }
        public DelegateCommand MakePhoneCallCommand { get; set; }

        public AdvisoryTabViewModel(INavigationService navigationService, IMessageService messageService)
        {
            _messageService = messageService;
            _navigationService = navigationService;
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = new DelegateCommand<AdvisoryMessage>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
            ItemClickCommand = new DelegateCommand<AdvisoryMessage>(ExecuteItemClickCommand);
            SendAdvisoryMessageCommand = new DelegateCommand(ExecuteSendAdvisoryMessageCommand);
            TakeOrPickPhotoCommand = new DelegateCommand(ExecuteTakeOrPickPhotoCommandAsync);
            MakePhoneCallCommand = new DelegateCommand(ExecuteMakePhoneCallCommand);

            MessagingCenter.Subscribe<AdvisoryMessage>(this, "UpdateAdvisoryMessages", newMessage =>
            {
                _messages.Add(newMessage);
                LocalDataStorage.SetAdvisoryMessages(_messages.Select(Mapper.Map<AdvisoryMessageEntity>).ToList());
            });

            //if (App.HubConnection.State == ConnectionState.Disconnected)
            //{
            //    App.HubConnection.Start().Wait();
            //    App.HubProxy.Invoke("HandShake", App.CurrentUser.UserName).Wait();
            //}
            //if (!subscribed)
            //{
            //    App.HubProxy.Subscribe("PublishAdvisoryMessage").Received += rs => {
            //        var newMessage = rs[0]?.ToObject<AdvisoryMessage>();
            //        if (newMessage?.ConversationName == App.CurrentUser.UserName && newMessage.IsAdviser)
            //        {
            //            _messages.Add(newMessage);
            //            LocalDataStorage.SetAdvisoryMessages(_messages.Select(Mapper.Map<AdvisoryMessageEntity>).ToList());
            //        }
            //    };

            //    subscribed = true;
            //}
        }

        private void ExecuteMakePhoneCallCommand()
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(LocalDataStorage.GetHotLine(), "Vnap");
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async void ExecuteRefreshCommand()
        {
            IsBusy = true;

            _messages = new ObservableRangeCollection<AdvisoryMessage>();
            await _messageService.Sync(App.CurrentUser.UserName);
            LoadMessages(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(AdvisoryMessage item)
        {
            return IsNotBusy && _messages.Count < _totalMessages;
        }

        public void ExecuteLoadMoreCommand(AdvisoryMessage item)
        {
            IsBusy = true;

            var skip = _messages.Count;
            LoadMessages(skip);

            IsBusy = false;
        }

        public async void ExecuteItemClickCommand(AdvisoryMessage item)
        {
            if (!string.IsNullOrEmpty(item.ImageUrl))
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("ImageUrl", item.ImageUrl);
                await _navigationService.NavigateAsync($"PinchToZoomPage", navigationParams, animated: false, useModalNavigation: true);
            }
        }

        private async void ExecuteSendAdvisoryMessageCommand()
        {
            if(App.CurrentUser.UserName == null)
            {
                if (await UserDialogs.Instance.ConfirmAsync("Vui lòng đăng nhập để gửi câu hỏi cho kỹ sư!", null, "Đăng nhập", "Hủy"))
                {
                    await _navigationService.NavigateAsync("SplashScreen", animated: false, useModalNavigation: true);
                }
                return;
            }
            UserDialogs.Instance.ShowLoading("Đang gửi...");
            var message = new AdvisoryMessage()
            {
                AuthorName = App.CurrentUser.UserName,
                Content = NewMessage
            };
            try
            {
                if (App.HubConnection.State == ConnectionState.Disconnected)
                {
                    App.HubConnection.Start().Wait();
                    App.HubProxy.Invoke("HandShake", App.CurrentUser.UserName).Wait();
                }

                var result = await App.HubProxy.Invoke<AdvisoryMessage>("SubscribeAdvisoryMessage", message);
                if (result != null)
                {
                    _messages.Add(result);
                    NewMessage = string.Empty;
                }
            }
            catch (Exception e)
            {
            }
            UserDialogs.Instance.HideLoading();
        }

        private async void ExecuteTakeOrPickPhotoCommandAsync()
        {
            try
            {
                if (App.CurrentUser.UserName == null)
                {
                    if (await UserDialogs.Instance.ConfirmAsync("Vui lòng đăng nhập để gửi câu hỏi cho kỹ sư!", null, "Đăng nhập", "Hủy"))
                    {
                        await _navigationService.NavigateAsync("SplashScreen", animated: false, useModalNavigation: true);
                    }
                    return;
                }

                MediaFile file = null;
                var takePhotoString = "Chụp ảnh";
                var pickFromGalleryString = "Chọn từ thư viện";
                var action = await UserDialogs.Instance.ActionSheetAsync("", "", "", null, takePhotoString, pickFromGalleryString, "Hủy");

                if (action == takePhotoString)
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        UserDialogs.Instance.Alert("Vnap cần quyền truy cập Camera của bạn để thực hiện chức năng này!");
                        return;
                    }

                    file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                        AllowCropping = true,
                        PhotoSize = PhotoSize.Medium
                    });

                    if (file == null)
                        return;
                }

                if (action == pickFromGalleryString)
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        UserDialogs.Instance.Alert("Vnap cần quyền truy cập Camera của bạn để thực hiện chức năng này!");
                        return;
                    }
                    file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                        PhotoSize = PhotoSize.Medium
                    });

                    if (file == null)
                        return;
                }

                if (file != null)
                {
                    UserDialogs.Instance.ShowLoading("Đang gửi...");
                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(file.GetStream()), "\"file\"", $"AM_{App.CurrentUser.UserName}_{DateTime.Now.Ticks}.png");
                    var result = await _httpClient.PostAsync($"http://vnap.vn/api/advisorymessage/upload?authorName={App.CurrentUser.UserName}", content);
                    if (result.IsSuccessStatusCode)
                    {
                        var contents = await result.Content.ReadAsStringAsync();
                        if (contents != null)
                        {
                            var advisoryMessage = JsonConvert.DeserializeObject<AdvisoryMessage>(contents);
                            _messages.Add(advisoryMessage);
                        }
                    }
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public void LoadMessages(int skip)
        {
            var rq = new GetMessagesRq()
            {
                Skip = skip,
                ConversationName = App.CurrentUser.UserName
            };
            var newMessages = _messageService.GetMessages(rq);
            _totalMessages = _messageService.GetMessagesCount();

            Messages.AddRange(newMessages.Select(Mapper.Map<AdvisoryMessage>));

            if (!Messages.Any())
            {
                Messages.Add(new AdvisoryMessage()
                {
                    AuthorName = "Vnap",
                    ConversationName = rq.ConversationName,
                    Content = "Kính chào quý bà con. Đây là nơi để bà con gửi tin nhắn, hình ảnh sâu bệnh về tổng đài cho kỹ sư tư vấn.",
                    IsAdviser = true,
                    IsIntro = true,
                    CreatedDate = DateTime.Now
                });
            }
        }

        public void MessageListItemSelectedHandler(AdvisoryMessage message)
        {
            if (!string.IsNullOrEmpty(message.ImageUrl))
            {
                _navigationService.NavigateAsync(message.ImageUrl, animated: false);
            }
        }

        public async Task<bool> LoginRequestHandler()
        {
            if (App.CurrentUser.UserName == null)
            {
                if (await UserDialogs.Instance.ConfirmAsync("Vui lòng đăng nhập để gửi câu hỏi cho kỹ sư!", null, "Đăng nhập", "Hủy"))
                {
                    await _navigationService.NavigateAsync("SplashScreen", animated: false, useModalNavigation: true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
