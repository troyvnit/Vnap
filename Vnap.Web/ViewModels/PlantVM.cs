using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.ViewModels
{
    public class PlantVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
