using System;

namespace Vnap.Web.DataAccess.Entity
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsActived { get; set; }

        public ArticleCategory ArticleCategory { get; set; }
    }
}
