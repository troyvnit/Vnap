using System.Collections.Generic;

namespace Vnap.Web.DataAccess.Entity
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
