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

namespace Vnap.Service
{
    public interface IPlantDiseaseService
    {
        Task Sync();
        Task<List<PlantDisease>> GetPlantDiseases(GetPlantDiseasesRq rq);
        Task<int> GetPlantDiseasesCount();
        Task<PlantDisease> GetPlantDisease(string name);
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
                var getPlantDiseasesRs = await httpClient.GetStringAsync("http://210.245.27.38:6789/api/plantdisease");
                var plantDiseases = JsonConvert.DeserializeObject<List<PlantDisease>>(getPlantDiseasesRs);
                foreach (var plantDisease in plantDiseases)
                {
                    var existedPlantDisease = await _plantDiseaseRepository.Get(plantDisease.Id);
                    if (existedPlantDisease != null)
                    {
                        await _plantDiseaseRepository.Update(plantDisease);
                    }
                    else
                    {
                        await _plantDiseaseRepository.Insert(plantDisease);
                    }

                    foreach (var image in plantDisease.Images)
                    {
                        var existedImage = await _imageRepository.Get(image.Id);
                        if (existedImage != null)
                        {
                            await _imageRepository.Update(image);
                        }
                        else
                        {
                            await _imageRepository.Insert(image);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        private void FillContainer()
        {
            var startDate = new DateTime(2016, 1, 1);

            for (int i = 0; i < 200; i++)
            {
                _plantDiseaseRepository.Insert(new PlantDisease()
                {
                    Id = i,
                    Priority = i,
                    Name = "Đạo Ôn".ToUpper(),
                    Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                    CreatedDate = startDate.AddDays(i),
                    PlantId = 1
                });
            }
        }

        public async Task<List<PlantDisease>> GetPlantDiseases(GetPlantDiseasesRq rq)
        {
            var query = _plantDiseaseRepository.AsQueryable()
                .OrderBy(plantDisease => plantDisease.Priority)
                .OrderByDescending(plantDisease => plantDisease.CreatedDate);
            if (rq.Plant != null)
            {
                query = query.Where(plantDisease => plantDisease.PlantName.ToLower() == rq.Plant.ToLower());
            }
            if (rq.FromId > 0)
            {
                var fromPlantDisease = await _plantDiseaseRepository.Get(rq.FromId);
                query =
                    query.Where(
                        plantDisease => plantDisease.Priority >= fromPlantDisease.Priority && plantDisease.CreatedDate >= fromPlantDisease.CreatedDate);
            }
            query = query.Skip(rq.Skip);
            var plantDiseases = await query.ToListAsync();
            foreach (var plantDisease in plantDiseases)
            {
                var images = await _imageRepository.AsQueryable().Where(i => i.PlantDiseaseId == plantDisease.Id).ToListAsync();
                plantDisease.Images = new ObservableCollection<Image>(images);
            }
            return plantDiseases;
        }

        public async Task<PlantDisease> SearchFirstPlantDisease(string query)
        {
            return await _plantDiseaseRepository.AsQueryable().Where(plantDisease => plantDisease.Name.Contains(query) || plantDisease.Description.Contains(query)).FirstOrDefaultAsync();
        }

        public async Task<PlantDisease> GetPlantDisease(string name)
        {
            var plantDisease = await _plantDiseaseRepository.AsQueryable().Where(pd => pd.Name.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
            var images = await _imageRepository.AsQueryable().Where(i => i.PlantDiseaseId == plantDisease.Id).ToListAsync();
            plantDisease.Images = new ObservableCollection<Image>(images);
            return plantDisease;
        }

        public async Task<int> GetPlantDiseasesCount()
        {
            return await _plantDiseaseRepository.AsQueryable().CountAsync();
        }
    }
}
