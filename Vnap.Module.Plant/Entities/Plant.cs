using System.Collections.Generic;
using Vnap.Core.DataAccess.Entity;

namespace Vnap.Module.Plant.Entities
{
    public class Plant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }

        public List<PlantDisease> PlantDiseases { get; set; } 
    }
}
