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
    public class SaleController : ControllerBase
    {
        #region Properties
        private readonly ISaleService contract;
        #endregion

        #region Constructor
        public SaleController(ISaleService contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Crud Methods

        [HttpGet]

        [ProducesResponseType(typeof(Response<List<Sale>>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<Sale>> response = new Response<List<Sale>>();
            List<Sale> Sales = contract.Get();
            response.Result = Sales;
            return Ok(Sales);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<Sale>>), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<Sale> response = new Response<Sale>();
            Sale Sales = contract.Get(id);
            response.Result = Sales;
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        public ActionResult Post(Sale model)
        {
            Response<Sale> response = new Response<Sale>();
            response = contract.Post(model);
            return Ok(response.Result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<Sale>), StatusCodes.Status200OK)]
        public ActionResult Delete(Sale model)
        {
            Response<Sale> response = new Response<Sale>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<Sale>), StatusCodes.Status200OK)]
        public ActionResult Put(Sale model)
        {
            Response<Sale> response = new Response<Sale>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<Sale>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }
    }
}
