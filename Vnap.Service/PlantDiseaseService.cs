using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
    public class PlantDiseaseService : IPlantDiseaseService
    {
        private IRepository<PlantDisease> _plantDiseaseRepository;
        public PlantDiseaseService(IRepository<PlantDisease> plantDiseaseRepository)
        {
            _plantDiseaseRepository = plantDiseaseRepository;
        }

        public async Task Sync()
        {
            var count = await _plantDiseaseRepository.AsQueryable().CountAsync();
            if (count == 0)
            {
                FillContainer();
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
                    Avatar = "daoon.jpg",
                    CreatedDate = startDate.AddDays(i),
                    PlantId = 1
                });
            }
        }

        public async Task<List<PlantDisease>> GetPlantDiseases(GetPlantDiseasesRq rq)
        {
            var query = _plantDiseaseRepository.AsQueryable()
                .OrderByDescending(plantDisease => plantDisease.Priority)
                .OrderByDescending(plantDisease => plantDisease.CreatedDate);
            if (rq.PlantId > 0)
            {
                query = query.Where(plantDisease => plantDisease.PlantId == rq.PlantId);
            }
            if (rq.FromId > 0)
            {
                var fromPlantDisease = await _plantDiseaseRepository.Get(rq.FromId);
                query =
                    query.Where(
                        plantDisease => plantDisease.Priority >= fromPlantDisease.Priority && plantDisease.CreatedDate >= fromPlantDisease.CreatedDate);
            }
            query = query.Skip(rq.Skip).Take(rq.Take);
            return await query.ToListAsync();
        }

        public async Task<PlantDisease> SearchFirstPlantDisease(string query)
        {
            return await _plantDiseaseRepository.AsQueryable().Where(plantDisease => plantDisease.Name.Contains(query) || plantDisease.Description.Contains(query)).FirstOrDefaultAsync();
        }

        public async Task<int> GetPlantDiseasesCount()
        {
            return await _plantDiseaseRepository.AsQueryable().CountAsync();
        }
    }
}
