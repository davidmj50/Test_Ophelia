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
    public class CategoryService : ICategoryService
    {
        #region Properties
        private readonly ICategoryRepository contract;
        #endregion

        #region Constructor
        public CategoryService(ICategoryRepository contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<Category>> GetAllPaged(PaginatorRequest model)
        {
            Response<PaginatorResponse<Category>> response = new Response<PaginatorResponse<Category>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<Category>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }

            IList<Category> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);
            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }
        public Response<IList<Category>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<Category>> response = new Response<IList<Category>>
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
        public Response<Category> Delete(Category entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Category>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<Category> Get()
        {
            return contract.GetAll().ToList();
        }
        public Category Get(int id)
        {
            return contract.GetById(id);
        }
        public Response<Category> Post(Category entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Category>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<Category> Put(Category entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Category>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }
        #endregion
    }
}
