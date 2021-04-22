using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        private readonly ApplicationDataContext context;

        public WalletRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }

        public Wallet GetPointsByIdUser(int idUser)
        {
            return this.context.Wallets.Where(c => c.UserId == idUser).FirstOrDefault();
        }
    }
}
