using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDataContext context;

        public ProductRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
