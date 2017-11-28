using Acr.UserDialogs;
using Microsoft.AspNet.SignalR.Client;
using Prism.Navigation;
using System.Linq;
using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class AdvisoryTab : ContentPage
    {
        public AdvisoryTab()
        {
            InitializeComponent();
        }

        private void MessageList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (MessageListView.SelectedItem == null) return;
            var context = BindingContext as AdvisoryTabViewModel;
            var selectedItem = MessageListView.SelectedItem as AdvisoryMessage;
            context?.MessageListItemSelectedHandler(selectedItem);
            MessageListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.LieFocus = true;

            var context = BindingContext as AdvisoryTabViewModel;
            if (context != null && !context.Messages.Any(m => !m.IsIntro)) context.LoadMessages(0);
            var last = MessageListView.ItemsSource.Cast<AdvisoryMessage>().LastOrDefault();
            if (last != null)
            {
                MessageListView.ScrollTo(last, ScrollToPosition.MakeVisible, true);
            }
        }

        protected override void OnDisappearing()
        {
            NewMessageEntry.Unfocus();
            base.OnDisappearing();
        }

        private async void NewMessage_Focused(object sender, FocusEventArgs e)
        {
            var context = BindingContext as AdvisoryTabViewModel;
            if (context != null)
            {
                var unfocused = await context.LoginRequestHandler();
                if (unfocused)
                {
                    var entry = sender as Entry;
                    entry?.Unfocus();
                }
            }
        }
    }
}
