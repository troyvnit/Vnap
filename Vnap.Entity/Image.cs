namespace Vnap.Entity
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string Caption { get; set; }
        public int PlantDiseaseId { get; set; }
    }
}
