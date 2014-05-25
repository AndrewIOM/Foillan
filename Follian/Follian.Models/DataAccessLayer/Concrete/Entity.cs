using Follian.Models.DataAccessLayer.Abstract;

namespace Follian.Models.DataAccessLayer.Concrete
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
