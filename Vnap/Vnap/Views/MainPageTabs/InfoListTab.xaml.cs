using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class InfoListTab : ContentPage
    {
        public InfoListTab()
        {
            InitializeComponent();
        }
        private void PostList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PostListView.SelectedItem == null) return;
            var context = BindingContext as InfoListTabViewModel;
            var selectedItem = PostListView.SelectedItem as Post;
            context?.PostListItemSelectedHandler(selectedItem);
            PostListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as InfoListTabViewModel;
            if (context != null) await context.LoadPosts(0);
            base.OnAppearing();
        }
    }
}
