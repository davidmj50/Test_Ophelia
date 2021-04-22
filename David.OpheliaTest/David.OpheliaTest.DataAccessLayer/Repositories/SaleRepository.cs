using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly ApplicationDataContext context;

        public SaleRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
