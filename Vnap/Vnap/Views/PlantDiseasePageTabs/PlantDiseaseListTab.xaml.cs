﻿using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseListTab : ContentPage
    {
        private bool _loaded;

        public PlantDiseaseListTab()
        {
            InitializeComponent();
        }

        private void PlantDiseaseList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PlantDiseaseListView.SelectedItem == null) return;
            var context = BindingContext as PlantDiseaseListTabViewModel;
            var selectedItem = PlantDiseaseListView.SelectedItem as PlantDisease;
            context?.PlantDiseaseListItemSelectedHandler(selectedItem);
            PlantDiseaseListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            if (!_loaded)
            {
                var context = BindingContext as PlantDiseaseListTabViewModel;
                if (context != null) await context.LoadPlantDiseases(0);
                _loaded = true;
            }
            base.OnAppearing();
        }
    }
}
