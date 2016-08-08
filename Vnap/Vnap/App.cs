using Vnap.ViewModels;
using Vnap.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace Vnap
{
    public class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("LeftMenu/Navigation/MainPage", animated: false);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<Navigation>();
            Container.RegisterTypeForNavigation<LeftMenu>();
            Container.RegisterTypeForNavigation<PlantListPage>();
            Container.RegisterTypeForNavigation<InfoPage>();
            Container.RegisterTypeForNavigation<AdvisoryPage>();
        }

        protected override void ConfigureModuleCatalog()
        {
            //ModuleCatalog.AddModule(new ModuleInfo(typeof(ModuleA.ModuleAModule)));
            //ModuleCatalog.AddModule(new ModuleInfo("ModuleA", typeof(ModuleA.ModuleAModule), InitializationMode.OnDemand));
        }
    }
}
