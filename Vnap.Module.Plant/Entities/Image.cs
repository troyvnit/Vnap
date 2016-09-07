using Vnap.Core.DataAccess.Entity;

namespace Vnap.Module.Plant.Entities
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string Caption { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
