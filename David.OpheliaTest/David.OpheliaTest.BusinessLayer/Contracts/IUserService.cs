using David.OpheliaTest.Common.Models;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.BusinessLayer.Contracts
{
    public interface IUserService : IService<User>
    {
        Response<User> GetUserByCredentials(string email, string password);
    }
}
