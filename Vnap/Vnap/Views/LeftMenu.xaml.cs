using Prism.Navigation;
using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class LeftMenu : MasterDetailPage, IMasterDetailPageOptions
    {
        public LeftMenu()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;

        private void AccountMenu_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(AccountMenuListView.SelectedItem == null) return;
            var context = BindingContext as LeftMenuViewModel;
            context?.AccountMenuItemSelectedHandler(AccountMenuListView.SelectedItem as LeftMenuItem);
            LeftMenuListView.SelectedItem = null;
        }

        private void LeftMenu_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LeftMenuListView.SelectedItem == null) return;
            var context = BindingContext as LeftMenuViewModel;
            context?.LeftMenuItemSelectedHandler(LeftMenuListView.SelectedItem as LeftMenuItem);
            AccountMenuListView.SelectedItem = null;
        }
    }
}