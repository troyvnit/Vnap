using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vnap.Models
{
    public class PlantDisease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public PlantDiseaseType PlantDiseaseType { get; set; }
        public ObservableCollection<Image> Images { get; set; }
        public ObservableCollection<Solution> Solutions { get; set; }
        public bool IsEven { get; set; }
    }

    public enum PlantDiseaseType
    {
        Pests = 0, Disease = 1
    }
}
