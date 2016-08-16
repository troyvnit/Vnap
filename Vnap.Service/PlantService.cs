using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Entity;
using Vnap.Repository;

namespace Vnap.Service
{
    public interface IPlantService
    {
        void Sync();
        Task<List<Plant>> Load(DateTime? fromDate);
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
                    Name = "Cây Lúa".ToUpper(),
                    Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                    CreatedDate = startDate.AddDays(i)
                });
            }
        }

        public async Task<List<Plant>> Load(DateTime? fromDate)
        {
            if (!fromDate.HasValue)
                return await _plantRepository.AsQueryable().OrderByDescending(plant => plant.CreatedDate).Take(20).ToListAsync();

            var count = await _plantRepository.AsQueryable().Where(o => o.CreatedDate < fromDate).CountAsync();
            if (count == 0)
                return new List<Plant>();

            return await _plantRepository.AsQueryable().Where(plant => plant.CreatedDate <= fromDate).OrderByDescending(plant => plant.CreatedDate).Take(20).ToListAsync();

        }
    }
}
