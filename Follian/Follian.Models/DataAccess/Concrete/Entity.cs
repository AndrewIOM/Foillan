using Foillan.Models.DataAccess.Abstract;

namespace Foillan.Models.DataAccess.Concrete
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
