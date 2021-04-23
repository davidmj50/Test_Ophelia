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
    public class ProductService : IProductService
    {
        #region Properties
        private readonly IProductRepository contract;
        #endregion

        #region Constructor
        public ProductService(IProductRepository contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<Product>> GetAllPaged(PaginatorRequest model)
        {
            Response<PaginatorResponse<Product>> response = new Response<PaginatorResponse<Product>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<Product>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }

            IList<Product> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);
            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }
        public Response<IList<Product>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<Product>> response = new Response<IList<Product>>
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
        public Response<Product> Delete(Product entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Product>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<Product> Get()
        {
            return contract.GetAll().ToList();
        }
        public Product Get(int id)
        {
            return contract.GetById(id);
        }
        public Response<Product> Post(Product entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Product>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<Product> Put(Product entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Product>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }

        public List<Product> GetProductsByCategory(int idCategory)
        {
            return this.contract.GetProductsByCategory(idCategory);
        }

        public List<Product> SearchProducts(string keywords)
        {
            return this.contract.SearchProducts(keywords);
        }
        #endregion
    }
}
