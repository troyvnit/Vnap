using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vnap.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string[] _tabs;

        public string[] Tabs
        {
            get { return _tabs; }
            set { SetProperty(ref _tabs, value); }
        }

        private List<string> listTabs = new List<string>() { "CAY TRONG", "THONG TIN", "TU VAN" };

        public MainPageViewModel()
        {

            _tabs = listTabs.ToArray();
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var indexOfTab = 1;
            if (parameters.ContainsKey("tab"))
            {
                var tab = (string) parameters["tab"];
                indexOfTab = listTabs.IndexOf(tab);
            }
            _tabs = new string[]
            {
                    listTabs[indexOfTab - 1],
                    listTabs[indexOfTab],
                    listTabs[indexOfTab + 1]
            };
        }
    }
}
