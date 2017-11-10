using System.Collections.Generic;

namespace Vnap.Web.DataAccess.Entity
{
    public class Solution : BaseEntity
    {
        public Solution()
        {
            PlantDiseases = new List<PlantDisease>();
        }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool Prime { get; set; }

        public int PlantDiseaseId { get; set; }
        //public PlantDisease PlantDisease { get; set; }

        public List<PlantDisease> PlantDiseases { get; set; }
    }
}
