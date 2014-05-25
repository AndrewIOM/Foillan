namespace Follian.Models.DataAccessLayer.Abstract
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
