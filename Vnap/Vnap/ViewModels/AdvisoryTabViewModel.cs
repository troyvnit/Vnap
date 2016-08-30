using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Message;
using Vnap.Service.Requests.Plant;

namespace Vnap.ViewModels
{
    public class AdvisoryTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages => _messages;

        private int _totalMessages;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Message> LoadMoreCommand { get; set; }

        public AdvisoryTabViewModel(INavigationService navigationService, IMessageService messageService)
        {
            _messageService = messageService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<Message>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            _messages = new ObservableCollection<Message>();
            await LoadMessages(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Message item)
        {
            return IsNotBusy && _messages.Count > _totalMessages;
        }

        public async Task ExecuteLoadMoreCommand(Message item)
        {
            IsBusy = true;

            var skip = _messages.Count;
            await LoadMessages(skip);

            IsBusy = false;
        }

        public async Task LoadMessages(int skip)
        {
            var rq = new GetMessagesRq()
            {
                Skip = skip
            };
            var newMessages = await _messageService.GetMessages(rq);
            _totalMessages = await _messageService.GetMessagesCount();
            
            foreach (var message in newMessages)
            {
                if (!_messages.Select(p => p.Id).Contains(message.Id))
                {
                    _messages.Add(Mapper.Map<Message>(message));
                }
            }
        }

        public void MessageListItemSelectedHandler(Message message)
        {
            _navigationService.NavigateAsync(message.NavigateUrl, animated: false);
        }
    }
}
