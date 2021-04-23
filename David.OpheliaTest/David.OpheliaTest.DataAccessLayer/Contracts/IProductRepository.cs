using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetProductsByCategory(int idCategory);
        List<Product> SearchProducts(string keywords);
    }
}
