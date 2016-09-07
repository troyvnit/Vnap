using System.ComponentModel.DataAnnotations;

namespace Vnap.Core.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
