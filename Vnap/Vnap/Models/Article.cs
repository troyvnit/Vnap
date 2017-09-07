using System;
using Vnap.Entity;

namespace Vnap.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public int Priority { get; set; }
        public string NavigateUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay => CreatedDate.ToString("dd/MM/yyyy HH:mm");
        public ArticleType ArticleType { get; set; }
        public bool IsActived { get; set; }
    }
}
