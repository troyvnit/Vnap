using System;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Vnap.ViewModels;
using Vnap.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using Vnap.Entity;
using Vnap.Mappers;
using Vnap.Repository;
using Vnap.Service;
using Vnap.Service.Utils;

namespace Vnap
{
    public class App : PrismApplication
    {
        private IPlantService _plantService;
        private IPlantDiseaseService _plantDiseaseService;
        private IPostService _postService;
        private IMessageService _messageService;

        private static User _currentUser = LocalDataStorage.GetUser();
        public static User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                LocalDataStorage.SetUser(value);
            }
        }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            AutoMapperConfiguration.Configure();
            NavigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);
            NavigationService.NavigateAsync("SplashScreen", animated: false, useModalNavigation: true);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<SplashScreen>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<Navigation>();
            Container.RegisterTypeForNavigation<LeftMenu>();
            Container.RegisterTypeForNavigation<PlantListTab>();
            Container.RegisterTypeForNavigation<InfoListTab>();
            Container.RegisterTypeForNavigation<AdvisoryTab>();
            Container.RegisterTypeForNavigation<NewsTab>();
            Container.RegisterTypeForNavigation<PlantDiseaseDetailTab>();
            Container.RegisterTypeForNavigation<PlantDiseasePage>();
            Container.RegisterTypeForNavigation<PlantDiseaseDetailPage>(); 
            Container.RegisterTypeForNavigation<PlantDiseaseSolutionPage>();
            Container.RegisterTypeForNavigation<PinchToZoomPage>();
            Container.RegisterType<ISyncService, SyncService>();
            Container.RegisterType<IPlantService, PlantService>();
            Container.RegisterType<IPlantDiseaseService, PlantDiseaseService>();
            Container.RegisterType<IPostService, PostService>();
            Container.RegisterType<IMessageService, MessageService>();
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
    }
}
