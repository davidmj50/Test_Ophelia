using David.OpheliaTest.API.Controllers;
using David.OpheliaTest.Common.Models;
using David.OpheliaTest.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.UnitTests
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _userController { get; set; }
        public UserControllerTest()
        {
            //_userController = new UserController();
        }

        [TestMethod]
        public void login()
        {
            var response = _userController.Login("david", "david");
            //Assert.IsTrue();
        } 
    }
}
