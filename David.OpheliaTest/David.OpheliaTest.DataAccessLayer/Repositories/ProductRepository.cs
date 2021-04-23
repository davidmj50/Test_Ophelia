using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDataContext context;

        public ProductRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }

        public List<Product> GetProductsByCategory(int idCategory)
        {
            List<Product> products = (from p in context.Products
                                        where p.CategoryId == idCategory
                                        select p).ToList();
            return products;
        }

        public List<Product> SearchProducts(string keywords)
        {
            List<Product> products = (from p in context.Products
                                      where p.ProductName.Contains(keywords)
                                      select p).ToList();
            return products;
        }
    }
}
