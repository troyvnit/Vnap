﻿using System;
using Plugin.Messaging;
using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;
using Vnap.Service.Utils;

namespace Vnap.Views
{
    public partial class PlantListTab : ContentPage
    {
        public PlantListTab()
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
