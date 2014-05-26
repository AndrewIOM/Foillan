namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface IUnitOfWork
    {
        IFoillanContext DbContext { get; }
        int Save();
    }
}
