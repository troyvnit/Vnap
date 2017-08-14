using Plugin.Messaging;
using Vnap.Service.Utils;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseSolutionPage : ContentPage
    {
        public PlantDiseaseSolutionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var context = BindingContext as PlantDiseaseSolutionPageViewModel;
            context?.LoadSolutionDetail();
            base.OnAppearing();
        }

        private void FloatingActionButton_OnClicked(object sender, System.EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(LocalDataStorage.GetHotLine(), "Vnap");
        }
    }
}
