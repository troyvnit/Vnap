using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vnap.Web.ViewModels
{
    public class ImageVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Priority { get; set; }

        public int PlantDiseaseId { get; set; }
    }
}
