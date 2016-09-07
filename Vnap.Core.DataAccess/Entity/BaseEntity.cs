using System;
using System.ComponentModel.DataAnnotations;

namespace Vnap.Core.DataAccess.Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUser CreatedUser { get; set; }
    }
}
