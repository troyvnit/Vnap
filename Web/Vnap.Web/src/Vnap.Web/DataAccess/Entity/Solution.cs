namespace Vnap.Web.DataAccess.Entity
{
    public class Solution : BaseEntity
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool Prime { get; set; }
    }
}
