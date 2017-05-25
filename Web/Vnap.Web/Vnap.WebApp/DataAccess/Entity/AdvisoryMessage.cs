using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vnap.Web.DataAccess.Entity
{
    public class AdvisoryMessage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsAdviser { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}