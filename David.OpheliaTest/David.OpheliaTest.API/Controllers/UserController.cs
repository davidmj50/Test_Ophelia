using David.OpheliaTest.BusinessLayer.Contracts;
using David.OpheliaTest.Common.Models;
using David.OpheliaTest.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace David.OpheliaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties
        private readonly IUserService contract;
        #endregion

        #region Constructor
        public UserController(IUserService contract)
        {
            this.contract = contract;
        }
        #endregion

        [HttpPost]
        [Route("Login/{user}/{password}")]
        [ProducesResponseType(typeof(Response<User>), StatusCodes.Status200OK)]
        public ActionResult Login(string user, string password)
        {
            Response<User> response = new Response<User>();
            response = contract.GetUserByCredentials(user, password);
            return Ok(response);
        }
        #region Crud Methods

        [HttpGet]

        [ProducesResponseType(typeof(Response<List<User>>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<User>> response = new Response<List<User>>();
            List<User> Users = contract.Get();
            response.Result = Users;
            return Ok(Users);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<User>>), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<User> response = new Response<User>();
            User Users = contract.Get(id);
            response.Result = Users;
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<User>), StatusCodes.Status200OK)]
        public ActionResult Post(User model)
        {
            Response<User> response = new Response<User>();
            response = contract.Post(model);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<User>), StatusCodes.Status200OK)]
        public ActionResult Delete(User model)
        {
            Response<User> response = new Response<User>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<User>), StatusCodes.Status200OK)]
        public ActionResult Put(User model)
        {
            Response<User> response = new Response<User>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<User>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }
    }
}
