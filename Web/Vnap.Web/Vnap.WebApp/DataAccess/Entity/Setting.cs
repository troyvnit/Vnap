namespace Vnap.Web.DataAccess.Entity
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DataType DataType { get; set; }
    }

    public enum DataType
    {
        String, Numeric, Boolean, DateTime
    }
}
