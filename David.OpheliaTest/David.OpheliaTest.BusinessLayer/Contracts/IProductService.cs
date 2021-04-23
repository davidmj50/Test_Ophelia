using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.BusinessLayer.Contracts
{
    public interface IProductService : IService<Product>
    {
        List<Product> GetProductsByCategory(int idCategory);
        List<Product> SearchProducts(string keywords);
    }
}
