using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;

namespace Vnap.Web.ViewModels
{
    public class SolutionVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public int[] PlantDiseaseIds { get; set; }
        public int[] PlantIds { get; set; }
        public bool Prime { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
