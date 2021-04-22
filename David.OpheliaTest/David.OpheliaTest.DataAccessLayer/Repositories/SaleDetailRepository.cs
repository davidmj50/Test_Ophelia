using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class SaleDetailRepository : GenericRepository<SaleDetail>, ISaleDetailRepository
    {
        private readonly ApplicationDataContext context;

        public SaleDetailRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
