using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vnap.Web.ViewModels
{
    public class UserVM
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Plant { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
