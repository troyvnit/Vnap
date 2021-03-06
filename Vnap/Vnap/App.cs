﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Vnap.ViewModels;
using Vnap.Views;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using Vnap.Entity;
using Vnap.Mappers;
using Vnap.Models;
using Vnap.Repository;
using Vnap.Service;
using Vnap.Service.Utils;
using Vnap.Services;
using Xamarin.Forms;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace Vnap
{
    public class App : PrismApplication
    {
        private IPlantService _plantService;
        private IPlantDiseaseService _plantDiseaseService;
        private IArticleService _articleService;
        private IMessageService _messageService;
        public static string SearchKey;

        public static HubConnection HubConnection;
        public static IHubProxy HubProxy;
        public static bool IsPlayServicesAvailable;
        public static bool LieFocus;

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

        public static int LatestAdvisoryMessageId { get; set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            AutoMapperConfiguration.Configure();
            if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.UserName))
            {
                NavigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);
            }
            else
            {
                NavigationService.NavigateAsync("SplashScreen", animated: false, useModalNavigation: true);
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<SplashScreen>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<Navigation>();
            Container.RegisterTypeForNavigation<LeftMenu>();
            Container.RegisterTypeForNavigation<PlantListTab>();
            Container.RegisterTypeForNavigation<NoticeListTab>();
            Container.RegisterTypeForNavigation<AdvisoryTab>();
            Container.RegisterTypeForNavigation<NewsListTab>();
            Container.RegisterTypeForNavigation<PlantDiseaseDetailTab>();
            Container.RegisterTypeForNavigation<PlantDiseasePage>();
            Container.RegisterTypeForNavigation<PlantDiseaseDetailPage>(); 
            Container.RegisterTypeForNavigation<PlantDiseaseSolutionPage>();
            Container.RegisterTypeForNavigation<PlantDiseaseSolutionListPage>();
            Container.RegisterTypeForNavigation<PinchToZoomPage>();
            Container.RegisterTypeForNavigation<ArticleDetailPage>();
            Container.RegisterTypeForNavigation<TermsPage>();
            Container.RegisterType<ISyncService, SyncService>();
            Container.RegisterType<IPlantService, PlantService>();
            Container.RegisterType<IPlantDiseaseService, PlantDiseaseService>();
            Container.RegisterType<IArticleService, ArticleService>();
            Container.RegisterType<IMessageService, MessageService>();
            Container.RegisterType<ISolutionService, SolutionService>();
            Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }

        protected override void ConfigureModuleCatalog()
        {
            //ModuleCatalog.AddModule(new ModuleInfo(typeof(ModuleA.ModuleAModule)));
            //ModuleCatalog.AddModule(new ModuleInfo("ModuleA", typeof(ModuleA.ModuleAModule), InitializationMode.OnDemand));
        }

        protected override void OnResume()
        {
            DependencyService.Get<INotificationService>().ClearBadge();
        }

        protected override void OnStart()
        {
            DependencyService.Get<INotificationService>().ClearBadge();
        }
    }
}
