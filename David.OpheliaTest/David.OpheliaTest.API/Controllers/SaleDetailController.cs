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
    public class SaleDetailController : ControllerBase
    {
        #region Properties
        private readonly ISaleDetailService contract;
        private readonly ISaleService saleContract;
        private readonly IProductService productContract;
        #endregion

        #region Constructor
        public SaleDetailController(ISaleDetailService contract, ISaleService saleContract, IProductService productContract)
        {
            this.contract = contract;
            this.saleContract = saleContract;
            this.productContract = productContract;
        }
        #endregion

        #region Crud Methods

        [HttpGet]

        [ProducesResponseType(typeof(Response<List<SaleDetail>>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<SaleDetail>> response = new Response<List<SaleDetail>>();
            List<SaleDetail> SaleDetails = contract.Get();
            response.Result = SaleDetails;
            return Ok(SaleDetails);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<SaleDetail>>), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<SaleDetail> response = new Response<SaleDetail>();
            SaleDetail SaleDetails = contract.Get(id);
            response.Result = SaleDetails;
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<SaleDetail>), StatusCodes.Status200OK)]
        public ActionResult Post(SaleDetail model)
        {
            Response<SaleDetail> response = new Response<SaleDetail>();
            response = contract.Post(model);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<SaleDetail>), StatusCodes.Status200OK)]
        public ActionResult Delete(SaleDetail model)
        {
            Response<SaleDetail> response = new Response<SaleDetail>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<SaleDetail>), StatusCodes.Status200OK)]
        public ActionResult Put(SaleDetail model)
        {
            Response<SaleDetail> response = new Response<SaleDetail>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<SaleDetail>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }

        [HttpPost]
        [Route("SaveAllSalesDetail")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public ActionResult SaveAllSalesDetail(List<SaleDetail> saleDetails)
        {
            
            return Ok(this.contract.PostAll(saleDetails));
        }
    }
}
