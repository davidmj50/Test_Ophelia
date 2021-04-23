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
    public class ProductController : ControllerBase
    {
        #region Properties
        private readonly IProductService contract;
        #endregion

        #region Constructor
        public ProductController(IProductService contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Crud Methods

        [HttpGet]

        [ProducesResponseType(typeof(Response<List<Product>>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<Product>> response = new Response<List<Product>>();
            List<Product> Products = contract.Get();
            response.Result = Products;
            return Ok(Products);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<Product> response = new Response<Product>();
            Product Products = contract.Get(id);
            response.Result = Products;
            response.IsSuccess = true;
            return Ok(response.Result);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<Product>), StatusCodes.Status200OK)]
        public ActionResult Post(Product model)
        {
            Response<Product> response = new Response<Product>();
            response = contract.Post(model);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<Product>), StatusCodes.Status200OK)]
        public ActionResult Delete(Product model)
        {
            Response<Product> response = new Response<Product>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<Product>), StatusCodes.Status200OK)]
        public ActionResult Put(Product model)
        {
            Response<Product> response = new Response<Product>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<Product>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }

        [HttpGet]
        [Route("productsByCategory/{id}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public ActionResult ProductsByCategory(int id)
        {
            List<Product> response = this.contract.GetProductsByCategory(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchProduct/{keywords}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public ActionResult SearchProducts(string keywords)
        {
            List<Product> response = this.contract.SearchProducts(keywords);
            return Ok(response);
        }
    }
}
