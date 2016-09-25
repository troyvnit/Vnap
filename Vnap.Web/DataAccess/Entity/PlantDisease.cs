using System.Collections.Generic;
using Vnap.Web.DataAccess.Entity.Enums;

namespace Vnap.Web.DataAccess.Entity
{
    public class PlantDisease : BaseEntity
    {
        public PlantDisease()
        {
            Images = new List<Image>();
            Solutions = new List<Solution>();
        }
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
