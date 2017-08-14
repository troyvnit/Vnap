using System;

namespace Vnap.Entity
{
    public class SettingEntity : BaseEntity
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DataType DataType { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum DataType
    {
        String, Numeric, Boolean, DateTime
    }
}
