﻿using System;
using Plugin.Messaging;
using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;
using Vnap.Service.Utils;
using System.Linq;

namespace Vnap.Views
{
    public partial class NewsListTab : ContentPage
    {
        public NewsListTab()
        {
            InitializeComponent();
        }

        private void ArticleList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ArticleListView.SelectedItem == null) return;
            var context = BindingContext as NewsListTabViewModel;
            var selectedItem = ArticleListView.SelectedItem as Article;
            context?.ArticleListItemSelectedHandler(selectedItem);
            ArticleListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            var context = BindingContext as NewsListTabViewModel;
            if (context != null && !context.Articles.Any()) context.LoadArticles(0);
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
