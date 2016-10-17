using System;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Vnap.ViewModels;
using Vnap.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using Vnap.Mappers;
using Vnap.Repository;
using Vnap.Service;

namespace Vnap
{
    public class App : PrismApplication
    {
        private IPlantService _plantService;
        private IPlantDiseaseService _plantDiseaseService;
        private IPostService _postService;
        private IMessageService _messageService;
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            AutoMapperConfiguration.Configure();
            NavigationService.NavigateAsync("SplashScreen", animated: false);
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
            Container.RegisterTypeForNavigation<PlantDiseasePage>();
            Container.RegisterTypeForNavigation<PlantDiseaseDetailPage>(); 
            Container.RegisterTypeForNavigation<PlantDiseaseSolutionPage>();
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
