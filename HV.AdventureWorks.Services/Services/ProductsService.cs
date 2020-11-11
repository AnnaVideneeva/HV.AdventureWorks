using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Data.Interfaces;
using HV.AdventureWorks.Services.Interfaces;
using HV.AdventureWorks.Services.Models;

namespace HV.AdventureWorks.Services.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsProvider _productsProvider;
        private readonly IMapper _mapper;

        public ProductsService(
            IProductsProvider productsProvider,
            IMapper mapper)
        {
            _productsProvider = productsProvider;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetAll()
        {
            var list = _productsProvider.GetAll();

            return _mapper.Map<IQueryable<ProductEntity>, IEnumerable<Product>>(list);
        }

        public Product GetById(int id)
        {
            var entity = _productsProvider.GetById(id);

            return _mapper.Map<Product>(entity);
        }

        public Product Create(Product model)
        {
            var entity = _mapper.Map<ProductEntity>(model);
            var savedEntity = _productsProvider.Create(entity);

            return _mapper.Map<Product>(savedEntity);
        }

        public Product Update(Product model)
        {
            var entity = _mapper.Map<ProductEntity>(model);
            var savedEntity = _productsProvider.Update(entity);

            return _mapper.Map<Product>(savedEntity);
        }

        public void Delete(int id)
        {
            _productsProvider.Delete(id);
        }
    }
}
