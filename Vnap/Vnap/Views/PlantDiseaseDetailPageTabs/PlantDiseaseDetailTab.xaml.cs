using System;
using Plugin.Messaging;
using Vnap.ViewModels;
using Xamarin.Forms;
using Vnap.Service.Utils;

namespace Vnap.Views
{
    public partial class PlantDiseaseDetailTab : ContentPage
    {
        public PlantDiseaseDetailTab()
        {
            InitializeComponent();
        }

        private void FloatingActionButton_OnClicked(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(LocalDataStorage.GetHotLine(), "Vnap");
        }
    }
}
