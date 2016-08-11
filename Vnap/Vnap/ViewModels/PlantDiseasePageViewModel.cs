using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Vnap.ViewModels
{
    public class PlantDiseasePageViewModel : BaseViewModel
    {
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Title = (string)parameters[parameters.Keys.FirstOrDefault(k => k == "query")];
            base.OnNavigatedTo(parameters);
        }
    }
}
