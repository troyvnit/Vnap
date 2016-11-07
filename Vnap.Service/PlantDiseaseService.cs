using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public interface IPlantDiseaseService
    {
        Task Sync();
        List<PlantDisease> GetPlantDiseases(GetPlantDiseasesRq rq);
        int GetPlantDiseasesCount();
        PlantDisease GetPlantDisease(int id);
    }
    public class PlantDiseaseService : IPlantDiseaseService
    {
        HttpClient httpClient = new HttpClient();

        private IRepository<PlantDisease> _plantDiseaseRepository;
        private IRepository<Image> _imageRepository;
        public PlantDiseaseService(IRepository<PlantDisease> plantDiseaseRepository, IRepository<Image> imageRepository)
        {
            _plantDiseaseRepository = plantDiseaseRepository;
            _imageRepository = imageRepository;
        }

        public async Task Sync()
        {
            try
            {
                var getPlantDiseasesRs = await httpClient.GetStringAsync("http://vnap.vn/api/plantdisease");
                var plantDiseases = JsonConvert.DeserializeObject<List<PlantDisease>>(getPlantDiseasesRs);
                LocalDataStorage.SetPlantDiseases(plantDiseases);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public List<PlantDisease> GetPlantDiseases(GetPlantDiseasesRq rq)
        {
            var query = LocalDataStorage.GetPlantDiseases()
                .OrderBy(plantDisease => plantDisease.Priority)
                .ThenByDescending(plantDisease => plantDisease.CreatedDate)
                .AsQueryable();
            if (rq.Plant != null)
            {
                query = query.Where(plantDisease => plantDisease.PlantName.ToLower() == rq.Plant.ToLower());
            }
            var plantDiseases = query.ToList();

            return plantDiseases;
        }

        public PlantDisease SearchFirstPlantDisease(string query)
        {
            return LocalDataStorage.GetPlantDiseases().FirstOrDefault(plantDisease => plantDisease.Name.Contains(query) || plantDisease.Description.Contains(query));
        }

        public PlantDisease GetPlantDisease(int id)
        {
            var plantDisease = LocalDataStorage.GetPlantDiseases().FirstOrDefault(pd => pd.Id == id);
            return plantDisease;
        }

        public int GetPlantDiseasesCount()
        {
            var plantDiseases = LocalDataStorage.GetPlantDiseases();
            return plantDiseases.Count;
        }
    }
}
