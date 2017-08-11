using System;

namespace Vnap.Entity
{
    public class ArticleEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public ArticleType ArticleType { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum ArticleType
    {
        Introduction, Notice, News, Tutorial
    }
}
