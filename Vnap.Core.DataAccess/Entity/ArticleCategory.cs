using System.Collections.Generic;

namespace Vnap.Core.DataAccess.Entity
{
    public class ArticleCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public List<Article> Articles { get; set; }
    }
}
