using David.OpheliaTest.BusinessLayer.Contracts;
using David.OpheliaTest.Common.Helpers;
using David.OpheliaTest.Common.Models;
using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace David.OpheliaTest.BusinessLayer.Services
{
    public class WalletService : IWalletService
    {
        #region Properties
        private readonly IWalletRepository contract;
        #endregion

        #region Constructor
        public WalletService(IWalletRepository contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<Wallet>> GetAllPaged(PaginatorRequest model)
        {
            Response<PaginatorResponse<Wallet>> response = new Response<PaginatorResponse<Wallet>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<Wallet>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }

            IList<Wallet> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);
            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }
        public Response<IList<Wallet>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<Wallet>> response = new Response<IList<Wallet>>
            {
                IsSuccess = false

            };
            if (model == null)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser null" } };
                return response;
            }
            if (model.Query.Count() > 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
                return response;
            }
            response.Result = this.contract.GetAllMatched(model);
            response.IsSuccess = true;
            return response;
        }
        #endregion

        #region CRUD Methods
        public Response<Wallet> Delete(Wallet entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Wallet>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<Wallet> Get()
        {
            return contract.GetAll().ToList();
        }
        public Wallet Get(int id)
        {
            return contract.GetById(id);
        }
        public Response<Wallet> Post(Wallet entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Wallet>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<Wallet> Put(Wallet entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Wallet>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }

        public Wallet GetPointsByIdUser(int idUser)
        {
            return contract.GetPointsByIdUser(idUser);
        }
        #endregion
    }
}
