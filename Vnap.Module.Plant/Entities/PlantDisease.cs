using System.Collections.Generic;
using Vnap.Core.DataAccess.Entity;
using Vnap.Module.Plant.Entities.Enums;

namespace Vnap.Module.Plant.Entities
{
    public class PlantDisease : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public PlantDiseaseType PlantDiseaseType { get; set; }

        public List<Image> Images { get; set; } 

        public int PlantId { get; set; }
        public Plant Plant { get; set; }

        public List<Solution> Solutions { get; set; } 
    }
}
