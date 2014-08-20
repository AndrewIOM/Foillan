namespace Foillan.Models.DataAccess.Abstract
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
