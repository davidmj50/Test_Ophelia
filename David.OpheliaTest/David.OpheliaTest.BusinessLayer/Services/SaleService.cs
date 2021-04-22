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
    public class SaleService : ISaleService
    {
        #region Properties
        private readonly ISaleRepository contract;
        #endregion

        #region Constructor
        public SaleService(ISaleRepository contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<Sale>> GetAllPaged(PaginatorRequest model)
        {
            Response<PaginatorResponse<Sale>> response = new Response<PaginatorResponse<Sale>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<Sale>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }

            IList<Sale> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);
            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }
        public Response<IList<Sale>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<Sale>> response = new Response<IList<Sale>>
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
        public Response<Sale> Delete(Sale entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Sale>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<Sale> Get()
        {
            return contract.GetAll().ToList();
        }
        public Sale Get(int id)
        {
            return contract.GetById(id);
        }
        public Response<Sale> Post(Sale entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Sale>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<Sale> Put(Sale entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Sale>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }
        #endregion
    }
}
