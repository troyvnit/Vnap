using System;

namespace Vnap.Entity
{
    public class PostEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public PostType PostType { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum PostType
    {
        All, Info, News
    }
}
