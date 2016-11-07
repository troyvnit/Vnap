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
            Settings.Local.Set("Plants", plants);
        }

        public static List<Plant> GetPlants()
        {
            return Settings.Local.Get("Plants", new List<Plant>());
        }

        public static void SetPlantDiseases(List<PlantDisease> plantDiseases)
        {
            Settings.Local.Set("PlantDiseases", plantDiseases);
        }

        public static List<PlantDisease> GetPlantDiseases()
        {
            return Settings.Local.Get("PlantDiseases", new List<PlantDisease>());
        }

        public static void SetAdvisoryMessages(List<AdvisoryMessageEntity> messages)
        {
            Settings.Local.Set("AdvisoryMessages", messages);
        }

        public static List<AdvisoryMessageEntity> GetAdvisoryMessages()
        {
            return Settings.Local.Get("AdvisoryMessages", new List<AdvisoryMessageEntity>());
        }

        public static void SetPosts(List<PostEntity> posts)
        {
            Settings.Local.Set("Posts", posts);
        }

        public static List<PostEntity> GetPosts()
        {
            return Settings.Local.Get("Posts", new List<PostEntity>());
        }
    }
}
