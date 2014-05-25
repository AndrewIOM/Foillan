using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
