using System;
using Plugin.Messaging;
using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseListTab : ContentPage
    {
        public PlantDiseaseListTab()
        {
            InitializeComponent();
        }

        private void FloatingActionButton_OnClicked(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall("+84987575246", "Vnap");
        }
    }
}
