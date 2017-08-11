using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vnap.Web.ViewModels;

namespace Vnap.WebApp.ViewModels
{
    public class SyncResponseVM
    {
        public IEnumerable<PlantVM> Plants { get; set; }
        public IEnumerable<PlantDiseaseVM> PlantDiseases { get; set; }
        public IEnumerable<ArticleVM> Articles { get; set; }
        public IEnumerable<SettingVM> Settings { get; set; }
    }
}