using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Plant;

namespace Vnap.Service
{
    public interface IPlantService
    {
        Task Sync();
        Task<List<Plant>> GetPlants(GetPlantsRq rq);
        Task<int> GetPlantsCount();
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
                var getPlantsRs = await httpClient.GetStringAsync("http://210.245.27.38:6789/api/plant");
                var plants = JsonConvert.DeserializeObject<List<Plant>>(getPlantsRs);
                foreach (var plant in plants)
                {
                    var existedPlant = await _plantRepository.Get(plant.Id);
                    if (existedPlant != null)
                    {
                        await _plantRepository.Update(plant);
                    }
                    else
                    {
                        await _plantRepository.Insert(plant);
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

            _plantRepository.Insert(new Plant()
            {
                Id = 1,
                Priority = 2,
                Name = "Cây Lúa".ToUpper(),
                Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                Avatar = "http://vannghetiengiang.vn/uploads/news/2014_11/cay-lua2.jpg",
                CreatedDate = startDate.AddDays(1)
            });
            _plantRepository.Insert(new Plant()
            {
                Id = 2,
                Priority = 2,
                Name = "Hồ Tiêu".ToUpper(),
                Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(2)
            });
            _plantRepository.Insert(new Plant()
            {
                Id = 3,
                Priority = 3,
                Name = "Cà Phê".ToUpper(),
                Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                Avatar = "http://vannghetiengiang.vn/uploads/news/2014_11/cay-lua2.jpg",
                CreatedDate = startDate.AddDays(3)
            });
            _plantRepository.Insert(new Plant()
            {
                Id = 4,
                Priority = 4,
                Name = "Cây Có Múi".ToUpper(),
                Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                Avatar = "http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg",
                CreatedDate = startDate.AddDays(4)
            });
        }

        public async Task<List<Plant>> GetPlants(GetPlantsRq rq)
        {
            var query = _plantRepository.AsQueryable()
                .OrderBy(plant => plant.Priority)
                .OrderByDescending(plant => plant.CreatedDate);
            if (rq.FromId > 0)
            {
                var fromPlant = await _plantRepository.Get(rq.FromId);
                query =
                    query.Where(
                        plant => plant.Priority >= fromPlant.Priority && plant.CreatedDate >= fromPlant.CreatedDate);
            }
            query = query.Skip(rq.Skip);
            var plants = await query.ToListAsync();
            return plants;
        }

        public async Task<Plant> SearchFirstPlant(string query)
        {
            return await _plantRepository.AsQueryable().Where(plant => plant.Name.Contains(query) || plant.Description.Contains(query)).FirstOrDefaultAsync();
        }

        public async Task<int> GetPlantsCount()
        {
            return await _plantRepository.AsQueryable().CountAsync();
        }
    }
}
