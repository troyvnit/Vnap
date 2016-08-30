﻿using Microsoft.Practices.Unity;
using Vnap.ViewModels;
using Vnap.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using Vnap.Mappers;
using Vnap.Repository;
using Vnap.Service;
using Vnap.Views.Customs;

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
            Sync();
        }

        protected override void OnInitialized()
        {
            AutoMapperConfiguration.Configure();
            NavigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);
        }

        protected override void RegisterTypes()
        {
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

        public async void Sync()
        {
            await DatabaseHelper.InitialDatabase();
            _plantService = Container.Resolve<IPlantService>();
            await _plantService.Sync();

            _plantDiseaseService = Container.Resolve<IPlantDiseaseService>();
            await _plantDiseaseService.Sync();

            _postService = Container.Resolve<IPostService>();
            await _postService.Sync();

            _messageService = Container.Resolve<IMessageService>();
            await _messageService.Sync();
        }
    }
}
