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
            Children.Add(new PlantListPage());
            Children.Add(new InfoPage());
            Children.Add(new AdvisoryPage());
        }
    }
}
