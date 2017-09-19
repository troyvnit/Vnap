using System;
using Plugin.Messaging;
using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;
using Vnap.Service.Utils;

namespace Vnap.Views
{
    public partial class PlantDiseaseSolutionListPage : ContentPage
    {
        public PlantDiseaseSolutionListPage()
        {
            InitializeComponent();
        }

        private void PlantDiseaseSolutionList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PlantDiseaseSolutionListView.SelectedItem == null) return;
            var context = BindingContext as PlantDiseaseSolutionListPageViewModel;
            var selectedItem = PlantDiseaseSolutionListView.SelectedItem as Solution;
            context?.SolutionListItemSelectedHandler(selectedItem);
            PlantDiseaseSolutionListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void FloatingActionButton_OnClicked(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(LocalDataStorage.GetHotLine(), "Vnap");
        }
    }
}
