using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vnap.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdviser { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string NavigateUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
