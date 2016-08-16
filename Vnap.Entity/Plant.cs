using System;

namespace Vnap.Entity
{
    public class Plant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
