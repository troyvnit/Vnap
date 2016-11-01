namespace Vnap.Web.DataAccess.Entity
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string Caption { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Priority { get; set; }

        public int PlantDiseaseId { get; set; }
        public PlantDisease PlantDisease { get; set; }
    }
}
