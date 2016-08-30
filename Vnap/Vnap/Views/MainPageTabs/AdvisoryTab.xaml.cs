using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class AdvisoryTab : ContentPage
    {
        private bool _loaded;

        public AdvisoryTab()
        {
            InitializeComponent();
        }

        private void MessageList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (MessageListView.SelectedItem == null) return;
            //var context = BindingContext as AdvisoryTabViewModel;
            //var selectedItem = MessageListView.SelectedItem as Message;
            //context?.MessageListItemSelectedHandler(selectedItem);
            MessageListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            if (!_loaded)
            {
                var context = BindingContext as AdvisoryTabViewModel;
                if (context != null) await context.LoadMessages(0);
                _loaded = true;
            }
            base.OnAppearing();
        }
    }
}
