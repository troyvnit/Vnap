using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;

namespace Vnap.Web.ViewModels
{
    public class ArticleVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsActived { get; set; }
        public ArticleType ArticleType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
