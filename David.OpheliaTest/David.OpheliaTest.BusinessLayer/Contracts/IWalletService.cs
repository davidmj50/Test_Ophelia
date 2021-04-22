using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.BusinessLayer.Contracts
{
    public interface IWalletService : IService<Wallet>
    {
        Wallet GetPointsByIdUser(int id);
    }
}
