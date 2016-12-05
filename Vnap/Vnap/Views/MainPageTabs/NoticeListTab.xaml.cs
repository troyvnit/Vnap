using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class NoticeListTab : ContentPage
    {
        public NoticeListTab()
        {
            InitializeComponent();
        }

        private void ArticleList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ArticleListView.SelectedItem == null) return;
            var context = BindingContext as NoticeListTabViewModel;
            var selectedItem = ArticleListView.SelectedItem as Article;
            context?.ArticleListItemSelectedHandler(selectedItem);
            ArticleListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as NoticeListTabViewModel;
            if (context != null) await context.LoadArticles(0);
            base.OnAppearing();
        }
    }
}
