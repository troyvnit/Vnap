using System.Collections.Generic;
using Vnap.Core.DataAccess.Entity.Enums;

namespace Vnap.Core.DataAccess.Entity
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
        public Core.DataAccess.Entity.Plant Plant { get; set; }

        public List<Solution> Solutions { get; set; } 
    }
}
