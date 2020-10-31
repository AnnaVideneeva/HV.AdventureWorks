using System.Collections.Generic;
using System.Linq;

namespace HV.AdventureWorks.Core.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity: class
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetAsNoTracking();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IList<TEntity> entities);
        void Attach(TEntity entity);
    }
}
