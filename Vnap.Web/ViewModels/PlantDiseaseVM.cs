using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;

namespace Vnap.Web.ViewModels
{
    public class PlantDiseaseVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public PlantDiseaseType PlantDiseaseType { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Image> Images { get; set; }
    }
}
