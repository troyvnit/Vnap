using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Events;
using Vnap.Events;
using Vnap.Views;

namespace Vnap.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        INavigationService _navigationService;

        private string _title = "MainPage";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {

                SetProperty(ref _isActive, value);
            }
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate).ObservesCanExecute((p) => IsActive);

            ea.GetEvent<StringEvent>().Subscribe(Handled);
        }

        private void Handled(string obj)
        {
            Title = obj;
        }

        private void Navigate()
        {
            _navigationService.Navigate("LeftMenuPage");
        }
    }
}
