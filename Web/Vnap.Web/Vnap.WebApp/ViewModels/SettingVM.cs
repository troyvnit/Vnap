using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;

namespace Vnap.Web.ViewModels
{
    public class SettingVM
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DataType DataType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
