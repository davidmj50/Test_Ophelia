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
    public class RoleService : IRoleService
    {
        #region Properties
        private readonly IRoleRepository contract;
        #endregion

        #region Constructor
        public RoleService(IRoleRepository contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<Role>> GetAllPaged(PaginatorRequest model)
        {
            Response<PaginatorResponse<Role>> response = new Response<PaginatorResponse<Role>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<Role>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }

            IList<Role> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);
            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }
        public Response<IList<Role>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<Role>> response = new Response<IList<Role>>
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
        public Response<Role> Delete(Role entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Role>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<Role> Get()
        {
            return contract.GetAll().ToList();
        }
        public Role Get(int id)
        {
            return contract.GetById(id);
        }
        public Response<Role> Post(Role entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Role>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<Role> Put(Role entity)
        {
            if (entity == null)
            {
                return ResponseHelper<Role>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }
        #endregion
    }
}
