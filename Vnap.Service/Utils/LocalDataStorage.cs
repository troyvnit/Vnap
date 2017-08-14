using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.Settings;
using Vnap.Entity;

namespace Vnap.Service.Utils
{
    public class LocalDataStorage
    {
        public static void SetPlants(List<Plant> plants)
        {
            Settings.Current.Set("Plants", plants);
        }

        public static List<Plant> GetPlants()
        {
            return Settings.Current.Get("Plants", new List<Plant>());
        }

        public static void SetPlantDiseases(List<PlantDisease> plantDiseases)
        {
            Settings.Current.Set("PlantDiseases", plantDiseases);
        }

        public static List<PlantDisease> GetPlantDiseases()
        {
            return Settings.Current.Get("PlantDiseases", new List<PlantDisease>());
        }

        public static void SetAdvisoryMessages(List<AdvisoryMessageEntity> messages)
        {
            Settings.Current.Set("AdvisoryMessages", messages);
        }

        public static List<AdvisoryMessageEntity> GetAdvisoryMessages()
        {
            return Settings.Current.Get("AdvisoryMessages", new List<AdvisoryMessageEntity>());
        }

        public static void SetArticles(List<ArticleEntity> posts)
        {
            Settings.Current.Set("Articles", posts);
        }

        public static List<ArticleEntity> GetArticles()
        {
            return Settings.Current.Get("Articles", new List<ArticleEntity>());
        }

        public static void SetSettings(List<SettingEntity> posts)
        {
            Settings.Current.Set("Settings", posts);
        }

        public static List<SettingEntity> GetSettings()
        {
            return Settings.Current.Get("Settings", new List<SettingEntity>());
        }

        public static string GetHotLine()
        {
            var hotlineSetting = GetSettings().FirstOrDefault(s => s.Key == "[Android]Hotline");
            return hotlineSetting != null ? hotlineSetting.Value : "+84987575246";
        }

        public static void SetUser(User user)
        {
            Settings.Current.Set("User", user);
        }

        public static User GetUser()
        {
            return Settings.Current.Get("User", new User());
        }
    }
}
