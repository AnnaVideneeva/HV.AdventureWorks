namespace HV.AdventureWorks.Core.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity: class;
        void SaveChanges();
    }
}
