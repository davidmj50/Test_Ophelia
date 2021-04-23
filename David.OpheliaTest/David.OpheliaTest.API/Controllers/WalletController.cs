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
    public class WalletController : ControllerBase
    {
        #region Properties
        private readonly IWalletService contract;
        #endregion

        #region Constructor
        public WalletController(IWalletService contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Crud Methods

        [HttpGet]

        [ProducesResponseType(typeof(Response<List<Wallet>>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            Response<List<Wallet>> response = new Response<List<Wallet>>();
            List<Wallet> Wallets = contract.Get();
            response.Result = Wallets;
            return Ok(Wallets);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<Wallet>>), StatusCodes.Status200OK)]
        public ActionResult Get(int id)
        {
            Response<Wallet> response = new Response<Wallet>();
            Wallet Wallets = contract.Get(id);
            response.Result = Wallets;
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<Wallet>), StatusCodes.Status200OK)]
        public ActionResult Post(Wallet model)
        {
            Response<Wallet> response = new Response<Wallet>();
            response = contract.Post(model);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<Wallet>), StatusCodes.Status200OK)]
        public ActionResult Delete(Wallet model)
        {
            Response<Wallet> response = new Response<Wallet>();
            response = contract.Delete(model);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<Wallet>), StatusCodes.Status200OK)]
        public ActionResult Put(Wallet model)
        {
            Response<Wallet> response = new Response<Wallet>();
            response = contract.Put(model);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("GetPage")]
        [ProducesResponseType(typeof(Response<PaginatorResponse<Wallet>>), StatusCodes.Status200OK)]
        public ActionResult GetPage(PaginatorRequest model)
        {
            return Ok(this.contract.GetAllPaged(model));
        }


        [HttpGet]
        [Route("GetPointsUser/{idUser}")]
        [ProducesResponseType(typeof(Wallet), StatusCodes.Status200OK)]
        public ActionResult GetPage(int idUser)
        {
            Wallet wallet = this.contract.GetPointsUser(idUser);
            return Ok(wallet.Points);
        }
    }
}
