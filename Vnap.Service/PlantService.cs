using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Plant;
using Vnap.Service.Utils;

namespace Vnap.Service
{
    public interface IPlantService
    {
        Task Sync();
        List<Plant> GetPlants(GetPlantsRq rq);
        int GetPlantsCount();
    }
    public class PlantService : IPlantService
    {
        HttpClient httpClient = new HttpClient();
        private IRepository<Plant> _plantRepository;
        public PlantService(IRepository<Plant> plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task Sync()
        {
            try
            {
                var getPlantsRs = await httpClient.GetStringAsync("http://vnap.vn/api/plant");
                var plants = JsonConvert.DeserializeObject<List<Plant>>(getPlantsRs);
                LocalDataStorage.SetPlants(plants);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public List<Plant> GetPlants(GetPlantsRq rq)
        {
            var plants = LocalDataStorage.GetPlants()
                .OrderBy(plantDisease => plantDisease.Priority)
                .ThenByDescending(plantDisease => plantDisease.CreatedDate)
                .ToList();

            return plants;
        }

        public Plant SearchFirstPlant(string query)
        {
            return LocalDataStorage.GetPlants().FirstOrDefault(plant => plant.Name.Contains(query) || plant.Description.Contains(query));
        }

        public int GetPlantsCount()
        {
            var plants = LocalDataStorage.GetPlants();
            return plants.Count;
        }
    }
}
