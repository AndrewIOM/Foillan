namespace Foillan.Models.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        IFoillanContext DbContext { get; }
        int Save();
    }
}
