using System;
using Vnap.ViewModels;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Vnap.Views
{
    public partial class MainPage : ExtendedTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            var model = BindingContext as MainPageViewModel;
            if (model != null)
                foreach (var tab in model.Tabs)
                {
                    Children.Add(new APage(tab));
                }
            BackgroundColor = Color.White;
        }
    }
}
