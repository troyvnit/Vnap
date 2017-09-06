using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.ViewModels
{
    public class ConversationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public AdvisoryMessageVM LatestMessage { get; set; }
    }
}
