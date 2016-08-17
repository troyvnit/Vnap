using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Plant;

namespace Vnap.Service
{
    public interface IPlantService
    {
        void Sync();
        Task<List<Plant>> GetPlants(GetPlantsRq rq);
        Task<int> GetPlantsCount();
    }
    public class PlantService : IPlantService
    {
        private IRepository<Plant> _plantRepository;
        public PlantService(IRepository<Plant> plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public void Sync()
        {
            FillContainer();
        }

        private void FillContainer()
        {
            var startDate = new DateTime(2016, 1, 1);

            for (int i = 0; i < 200; i++)
            {
                _plantRepository.Insert(new Plant()
                {
                    Id = i,
                    Priority = i,
                    Name = "Cây Lúa".ToUpper(),
                    Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                    Avatar = "caylua.jpg",
                    CreatedDate = startDate.AddDays(i)
                });
            }
        }

        public async Task<List<Plant>> GetPlants(GetPlantsRq rq)
        {
            var query = _plantRepository.AsQueryable()
                .OrderByDescending(plant => plant.Priority)
                .OrderByDescending(plant => plant.CreatedDate);
            if (rq.FromId > 0)
            {
                var fromPlant = await _plantRepository.Get(rq.FromId);
                query =
                    query.Where(
                        plant => plant.Priority >= fromPlant.Priority && plant.CreatedDate >= fromPlant.CreatedDate);
            }
            query = query.Skip(rq.Skip).Take(rq.Take);
            return await query.ToListAsync();
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
