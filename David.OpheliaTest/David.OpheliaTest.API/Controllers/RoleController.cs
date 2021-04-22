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
    public class RoleController : ControllerBase
    {
        #region Properties
        private readonly IRoleService contract;
        #endregion

        #region Constructor
        public RoleController(IRoleService contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Crud Methods

        [HttpGet]

        [ProducesResponseType(typeof(Response<List<Role>>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<Role>> response = new Response<List<Role>>();
            List<Role> Roles = contract.Get();
            response.Result = Roles;
            return Ok(Roles);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<Role>>), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<Role> response = new Response<Role>();
            Role Roles = contract.Get(id);
            response.Result = Roles;
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<Role>), StatusCodes.Status200OK)]
        public ActionResult Post(Role model)
        {
            Response<Role> response = new Response<Role>();
            response = contract.Post(model);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<Role>), StatusCodes.Status200OK)]
        public ActionResult Delete(Role model)
        {
            Response<Role> response = new Response<Role>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<Role>), StatusCodes.Status200OK)]
        public ActionResult Put(Role model)
        {
            Response<Role> response = new Response<Role>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<Role>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }
    }
}
