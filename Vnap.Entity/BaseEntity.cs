using SQLite;

namespace Vnap.Entity
{
    public abstract class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
