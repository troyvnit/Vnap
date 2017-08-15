using System.Collections.Generic;

namespace Vnap.Web.DataAccess.Entity
{
    public class Conversation : BaseEntity
    {
        public Conversation()
        {
            AdvisoryMessages = new List<AdvisoryMessage>();
        }

        public string Name { get; set; }

        public string ConnectionId { get; set; }

        public List<AdvisoryMessage> AdvisoryMessages { get; set; }
    }
}
