using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Vnap.Entity;
using Vnap.Extensions;
using Vnap.Repository;
using Vnap.Service;
using Vnap.Service.Utils;
using Vnap.Views.Popups;
using Xamarin.Forms;
using Plugin.Messaging;

namespace Vnap.ViewModels
{
    public class SplashScreenViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISyncService _syncService;
        private readonly IPlantService _plantService;
        private readonly IPlantDiseaseService _plantDiseaseService;
        private readonly IArticleService _articleService;
        private readonly IMessageService _messageService;
        private readonly HttpClient _httpClient = new HttpClient();
        private int _loaded;

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _plant;

        public string Plant
        {
            get { return _plant; }
            set { SetProperty(ref _plant, value); }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string Address { get; set; }

        private ObservableCollection<string> _cities = new ObservableCollection<string>()
        {
            "An Giang",
            "Bà Rịa – Vũng Tàu",
            "Bắc Giang",
            "Bắc Kạn",
            "Bạc Liêu",
            "Bắc Ninh",
            "Bến Tre",
            "Bình Định",
            "Bình Dương",
            "Bình Phước",
            "Bình Thuận",
            "Cà Mau",
            "Cần Thơ",
            "Cao Bằng",
            "Đà Nẵng",
            "Đắk Lắk",
            "Đắk Nông",
            "Điện Biên",
            "Đồng Nai",
            "Đồng Tháp",
            "Gia Lai",
            "Hà Giang",
            "Hà Nam",
            "Hà Nội",
            "Hà Tĩnh",
            "Hải Dương",
            "Hải Phòng",
            "Hậu Giang",
            "Hòa Bình",
            "Hưng Yên",
            "Khánh Hòa",
            "Kiên Giang",
            "Kon Tum",
            "Lai Châu",
            "Lâm Đồng",
            "Lạng Sơn",
            "Lào Cai",
            "Long An",
            "Nam Định",
            "Nghệ An",
            "Ninh Bình",
            "Ninh Thuận",
            "Phú Thọ",
            "Phú Yên",
            "Quảng Bình",
            "Quảng Nam",
            "Quảng Ngãi",
            "Quảng Ninh",
            "Quảng Trị",
            "Sóc Trăng",
            "Sơn La",
            "Tây Ninh",
            "Thái Bình",
            "Thái Nguyên",
            "Thanh Hóa",
            "Thừa Thiên Huế",
            "Tiền Giang",
            "Hồ Chí Minh",
            "Trà Vinh",
            "Tuyên Quang",
            "Vĩnh Long",
            "Vĩnh Phúc",
            "Yên Bái",
        };

        public ObservableCollection<string> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        private ObservableCollection<string> _plants;

        public ObservableCollection<string> Plants
        {
            get { return _plants; }
            set { SetProperty(ref _plants, value); }
        }

        public DelegateCommand CityItemClickCommand { get; set; }
        public DelegateCommand PlantItemClickCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }

        public SplashScreenViewModel(INavigationService navigationService, ISyncService syncService,
            IPlantService plantService, IPlantDiseaseService plantDiseaseService, IArticleService articleService,
            IMessageService messageService)
        {
            _navigationService = navigationService;
            _syncService = syncService;
            _plantService = plantService;
            _plantDiseaseService = plantDiseaseService;
            _articleService = articleService;
            _messageService = messageService;

            CityItemClickCommand = new DelegateCommand(ExecuteCityItemClickCommand);
            PlantItemClickCommand = new DelegateCommand(ExecutePlantItemClickCommand);
            SignUpCommand = new DelegateCommand(ExecuteSignUpCommand);
        }

        public async Task ExecuteOpenCitiesPopupCommand()
        {
            var citiesPopup = new CitiesPopup();
            citiesPopup.BindingContext = this;
            await PopupNavigation.PushAsync(citiesPopup);
        }

        public async Task ExecuteOpenPlantsPopupCommand()
        {
            var plantsPopup = new PlantsPopup();
            plantsPopup.BindingContext = this;
            await PopupNavigation.PushAsync(plantsPopup);
        }

        private async void ExecuteCityItemClickCommand()
        {
            await PopupNavigation.PopAsync();
        }

        private async void ExecutePlantItemClickCommand()
        {
            await PopupNavigation.PopAsync();
        }

        private async void ExecuteSignUpCommand()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                UserDialogs.Instance.Alert("Vui lòng nhập số điện thoại!");
                return;
            }
            if (string.IsNullOrEmpty(Plant))
            {
                UserDialogs.Instance.Alert("Vui lòng chọn cây trồng!");
                return;
            }
            if (string.IsNullOrEmpty(City))
            {
                UserDialogs.Instance.Alert("Vui lòng chọn tỉnh / thành phố!");
                return;
            }
            var user = new User()
            {
                UserName = UserName, Address = Address, City = City, Plant = Plant
            };

            UserDialogs.Instance.ShowLoading("Đang đăng ký...");

            var result = await _httpClient.PostAsync("http://vnap.vn/api/user/create", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            UserDialogs.Instance.HideLoading();

            if (!result.IsSuccessStatusCode)
            {
                var retry = await UserDialogs.Instance.ConfirmAsync("Đăng ký thất bại! Vui lòng thử lại hoặc liên hệ đường dây nóng +84987575246!", null, "Gọi", "Bỏ qua");
                if (retry)
                {
                    var phoneDialer = CrossMessaging.Current.PhoneDialer;
                    if (phoneDialer.CanMakePhoneCall)
                        phoneDialer.MakePhoneCall("+84987575246", "Vnap");
                    return;
                }
            }
            
            App.CurrentUser = user;
            await _navigationService.GoBackAsync(useModalNavigation: true);
        }

        public async Task SyncData()
        {

            var user = App.CurrentUser;
            if (!string.IsNullOrEmpty(user?.UserName) && !string.IsNullOrEmpty(user.City) &&
                !string.IsNullOrEmpty(user.Plant))
            {
                await _navigationService.GoBackAsync(useModalNavigation: true);
                await Task.Run(async () =>
                {
                    var syncResult = await _syncService.Sync();
                });
            }
            else
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Tải dữ liệu...");

                    var syncResult = await _syncService.Sync();
                    if (syncResult.Plants != null)
                    {
                        _plants = syncResult.Plants.Select(p => p.Name).ToObservableCollection();
                    }

                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetPositionAsync(10000);
                    var result = await _httpClient.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={position.Latitude},{position.Longitude}&key=AIzaSyCKO7qZp5U-WCmCNBbuvw6-psle2hi21lg");
                    var jObject = JObject.Parse(result);
                    var address = (string)jObject["results"][0]["formatted_address"];
                    Address = address;
                    var city = _cities.FirstOrDefault(c => address.ToLower().Contains(c.ToLower()));
                    if (!string.IsNullOrEmpty(city))
                    {
                        City = city;
                    }

                    UserDialogs.Instance.HideLoading();
                }
                catch (Exception)
                {
                    UserDialogs.Instance.Alert("Vùi lòng bật tính năng GPS!");
                }
            }
            //ImageService.Instance.LoadUrl("http://vannghetiengiang.vn/uploads/news/2014_11/cay-lua2.jpg", TimeSpan.FromDays(3))
            //.Success(async (size, loadingResult) =>
            //{
            //    _loaded++;
            //    await Navigate();
            //})
            //.Error(async exception =>
            //{
            //    _loaded++;
            //    await Navigate();
            //}).Preload();
            //ImageService.Instance.LoadUrl("http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg", TimeSpan.FromDays(3))
            //.Success(async (size, loadingResult) =>
            //{
            //    _loaded++;
            //    await Navigate();
            //})
            //.Error(async exception =>
            //{
            //    _loaded++;
            //    await Navigate();
            //}).Preload();
        }

        private async Task Navigate()
        {
            if(_loaded == 2)
            await _navigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);
        }
    }

    public class City
    {
        public string Name { get; set; }
    }
}
