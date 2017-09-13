using Microsoft.AspNet.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Service;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ISyncService _syncService;

        public MainPageViewModel(ISyncService syncService)
        {
            _syncService = syncService;

            App.HubConnection = new HubConnection("http://vnap.vn/");
            App.HubProxy = App.HubConnection.CreateHubProxy("NotificationHub");

            var message = new NotificationMessage();
            MessagingCenter.Send(message, "NotificationBackgroundService");
        }

        public async Task SyncData()
        {
            await Task.Run(async () =>
            {
                var syncResult = await _syncService.Sync(App.CurrentUser.UserName);
            });
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}
