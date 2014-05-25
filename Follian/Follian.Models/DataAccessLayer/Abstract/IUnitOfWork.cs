namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface IUnitOfWork
    {
        BiodiversityDbContext DbContext { get; }
        int Save();
    }
}
