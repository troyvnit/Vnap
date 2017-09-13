using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vnap.Service.Requests
{
    public abstract class BaseRq
    {
        protected BaseRq()
        {
            Take = 10;
        }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
