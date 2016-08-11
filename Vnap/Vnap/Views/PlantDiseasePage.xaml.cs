using System;
using Vnap.ViewModels;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Vnap.Views
{
    public partial class PlantDiseasePage : ExtendedTabbedPage
    {
        public PlantDiseasePage()
        {
            InitializeComponent();
            Children.Add(new PlantDiseaseTab());
            Children.Add(new PlantDiseaseTab());
            Children.Add(new PlantDiseaseTab());
        }
    }
}
