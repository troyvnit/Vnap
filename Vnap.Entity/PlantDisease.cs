using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;

namespace Vnap.Entity
{
    public class PlantDisease : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public PlantDiseaseType PlantDiseaseType { get; set; }
        [Ignore]
        public ObservableCollection<Image> Images { get; set; }
    }

    public enum PlantDiseaseType
    {
        Pests = 0, Disease = 1
    }
}
