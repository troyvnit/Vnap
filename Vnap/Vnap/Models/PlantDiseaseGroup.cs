using Vnap.Views.ExtendedControls;

namespace Vnap.Models
{
    public class PlantDiseaseGroup : ObservableRangeCollection<PlantDisease>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Icon { get; set; }
    }
}
