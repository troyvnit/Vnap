namespace Vnap.Service.Requests.Plant
{
    public class GetPlantDiseasesRq : BaseRq
    {
        public int FromId { get; set; }
        public string Plant { get; set; }
    }
}
