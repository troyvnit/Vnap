using System;

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
    }
}
