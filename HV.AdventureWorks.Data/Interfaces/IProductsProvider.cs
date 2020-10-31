using System.Linq;
using HV.AdventureWorks.Data.Entities;

namespace HV.AdventureWorks.Data.Interfaces
{
    public interface IProductsProvider
    {
        IQueryable<ProductEntity> GetAll();
        ProductEntity GetById(int id);
        ProductEntity Create(ProductEntity entity);
        ProductEntity Update(ProductEntity entity);
        void Delete(int id);
    }
}
