using David.OpheliaTest.BusinessLayer.Contracts;
using David.OpheliaTest.Common.Models;
using David.OpheliaTest.Entities;
using Microsoft.AspNetCore.Cors;
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
    //[EnableCors("localTests")]
    public class CategoryController : ControllerBase
    {
        #region Properties
        private readonly ICategoryService contract;
        #endregion

        #region Constructor
        public CategoryController(ICategoryService contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Crud Methods

        [HttpGet]
        //[Route("categories")]

        [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<Category>> response = new Response<List<Category>>();
            List<Category> Categorys = contract.Get();
            response.Result = Categorys;
            return Ok(Categorys);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<Category>>), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<Category> response = new Response<Category>();
            Category Categorys = contract.Get(id);
            response.Result = Categorys;
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status200OK)]
        public ActionResult Post(Category model)
        {
            Response<Category> response = new Response<Category>();
            response = contract.Post(model);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status200OK)]
        public ActionResult Delete(Category model)
        {
            Response<Category> response = new Response<Category>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status200OK)]
        public ActionResult Put(Category model)
        {
            Response<Category> response = new Response<Category>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<Category>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }
    }
}
