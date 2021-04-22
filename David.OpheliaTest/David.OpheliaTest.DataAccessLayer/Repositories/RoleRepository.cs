using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDataContext context;

        public RoleRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
