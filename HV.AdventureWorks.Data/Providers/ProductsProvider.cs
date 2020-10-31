using System.Linq;
using HV.AdventureWorks.Core.Data.Interfaces;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Data.Interfaces;

namespace HV.AdventureWorks.Data.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<ProductEntity> GetAll()
        {
            return _unitOfWork.Repository<ProductEntity>().GetAsNoTracking();
        }

        public ProductEntity GetById(int id)
        {
            return _unitOfWork.Repository<ProductEntity>().GetAsNoTracking()
                .First(entity => entity.Id == id);
        }

        public ProductEntity Create(ProductEntity entity)
        {
            _unitOfWork.Repository<ProductEntity>().Add(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }

        public ProductEntity Update(ProductEntity entity)
        {
            _unitOfWork.Repository<ProductEntity>().Attach(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetAll()
                .FirstOrDefault(entity => entity.Id == id);

            _unitOfWork.Repository<ProductEntity>().Delete(entity);
            _unitOfWork.SaveChanges();
        }
    }
}
