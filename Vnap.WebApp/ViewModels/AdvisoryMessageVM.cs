using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.ViewModels
{
    public class AdvisoryMessageVM
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdviser { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public int ConversationId { get; set; }
        public string ConversationName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
