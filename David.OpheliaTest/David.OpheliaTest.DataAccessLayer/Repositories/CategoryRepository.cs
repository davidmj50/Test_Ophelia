using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDataContext context;

        public CategoryRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
