using System;
using System.Collections.Generic;
using System.Linq;
using Vnap.Models;

namespace Vnap.Services
{
    class PlantService
    {
        List<Plant> _plantsContainer;

        public PlantService()
        {

            _plantsContainer = new List<Plant>(FillContainer());

        }

        private IEnumerable<Plant> FillContainer()
        {
            var random = new Random(0);
            var startDate = new DateTime(2016, 1, 1);

            for (int i = 0; i < 200; i++)
            {
                yield return new Plant()
                {
                    Id = i,
                    Name = "Cây Lúa".ToUpper(),
                    Description = "Được trồng ở các tỉnh Tây Nam Bộ".ToUpper(),
                    CreatedDate = startDate.AddDays(i)
                };
            }
        }

        public IEnumerable<Plant> Load(DateTime? fromDate)
        {
            if (!fromDate.HasValue)
                return _plantsContainer.OrderByDescending(plant => plant.CreatedDate).Take(20);

            if (!_plantsContainer.Any(o => o.CreatedDate < fromDate))
                return new List<Plant>();

            return _plantsContainer.Where(plant => plant.CreatedDate <= fromDate).OrderByDescending(plant => plant.CreatedDate).Take(20);

        }
    }
}
