using System.Collections.Generic;
using HV.AdventureWorks.Services.Models;

namespace HV.AdventureWorks.Services.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Create(Product model);
        Product Update(Product model);
        void Delete(int id);
    }
}
