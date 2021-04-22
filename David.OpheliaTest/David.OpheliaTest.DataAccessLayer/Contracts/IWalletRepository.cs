﻿using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.DataAccessLayer.Contracts
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Wallet GetPointsByIdUser(int idUser);
    }
}
