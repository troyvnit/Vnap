using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Service;

namespace Vnap.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ISyncService _syncService;

        public MainPageViewModel(ISyncService syncService)
        {
            _syncService = syncService;
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
