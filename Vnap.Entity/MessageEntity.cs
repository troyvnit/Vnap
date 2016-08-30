using System;

namespace Vnap.Entity
{
    public class MessageEntity : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsAdviser { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string NavigateUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
