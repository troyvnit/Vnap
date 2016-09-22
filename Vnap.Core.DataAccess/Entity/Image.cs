namespace Vnap.Core.DataAccess.Entity
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string Caption { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
