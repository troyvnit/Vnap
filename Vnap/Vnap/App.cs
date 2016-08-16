using Microsoft.Practices.Unity;
using Vnap.ViewModels;
using Vnap.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using Vnap.Repository;
using Vnap.Service;

namespace Vnap
{
    public class App : PrismApplication
    {
        private IPlantService _plantService;
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            Sync();
        }

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
            Container.RegisterTypeForNavigation<PlantDiseasePage>();
            Container.RegisterType<IPlantService, PlantService>();
            Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }

        protected override void ConfigureModuleCatalog()
        {
            //ModuleCatalog.AddModule(new ModuleInfo(typeof(ModuleA.ModuleAModule)));
            //ModuleCatalog.AddModule(new ModuleInfo("ModuleA", typeof(ModuleA.ModuleAModule), InitializationMode.OnDemand));
        }

        public void Search(string query)
        {
            NavigationService.NavigateAsync($"Navigation/PlantDiseasePage?query={query}", animated: false);
        }

        public async void Sync()
        {
            await DatabaseHelper.InitialDatabase();
            _plantService = Container.Resolve<IPlantService>();
            _plantService.Sync();
        }
    }
}
