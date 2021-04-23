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
    public class UserService : IUserService
    {
        #region Properties
        private readonly IUserRepository contract;
        #endregion

        #region Constructor
        public UserService(IUserRepository contract)
        {
            this.contract = contract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<User>> GetAllPaged(PaginatorRequest model)
        {
            IList<User> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);

            Response<PaginatorResponse<User>> response = new Response<PaginatorResponse<User>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<User>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }
            if (model.Query.Count > 0)
            {
                paginatorResponse = contract.GetAllPagedMatchedsIncluding(model.PageIndex, model.PageSize, out int _totalCount, model.Query, c => c.Role);
                totalCount = _totalCount;
            }
            else
            {
                paginatorResponse = contract.GetAllPagedIncluding(model.PageIndex, model.PageSize, out int _totalCount, c => c.Role);
                totalCount = _totalCount;
            }

            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }

        public Response<IList<User>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<User>> response = new Response<IList<User>>
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

        public Response<User> GetUserByEmail(string email)
        {
            Response<User> response = new Response<User> { IsSuccess = false };
            response.Result = this.contract.GetAllMatched(c => c.Email == email).FirstOrDefault();
            response.IsSuccess = true;
            return response;
        }
        #endregion

        #region CRUD Methods

        public Response<User> Delete(User entity)
        {
            if (entity == null)
            {
                return ResponseHelper<User>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<User> Get()
        {
            return contract.GetAllIncluding(c => c.Role).ToList();
        }
        public User Get(int id)
        {
            return contract.GetByIdAndIncluding(id, c => c.Role).FirstOrDefault();
        }

        public Response<User> GetUserByCredentials(string user, string pass)
        {
            Response<User> response = new Response<User>
            {
                IsSuccess = false,
            };

            try
            {
                response.Result = this.contract
                        .GetAllMatchedIncluding(c => c.Password == pass &&
                                    c.UserName == user &&
                                    c.Active == true, c => c.Role
                                    )
                        .FirstOrDefault();
                if (response.Result != null)
                {
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult { Message = $"Error al Consultar Usuario: {ex.ToString()}" });
            }
            return response;

        }

        public Response<User> ValidatePasswordUser(string oldPass, string newPass)
        {
            Response<User> response = new Response<User>
            {
                IsSuccess = false,
            };

            try
            {
                response.Result = this.contract
                        .GetAllMatchedIncluding(c => c.Password == oldPass &&
                                    c.Active == true, c => c.Role
                                    )
                        .FirstOrDefault();
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult { Message = $"Error al Consultar Usuario: {ex.ToString()}" });
            }
            return response;

        }

        public Response<User> Post(User entity)
        {
            if (entity == null)
            {
                return ResponseHelper<User>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<User> Put(User entity)
        {
            if (entity == null)
            {
                return ResponseHelper<User>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }

        public Response<bool> ValidatePasswordUser(string pass, int idUser)
        {
            Response<bool> response = new Response<bool>
            {
                IsSuccess = false,
                Result = false
            };

            try
            {
                Response<User> userResponse = this.GetUserById(idUser);
                if (userResponse.IsSuccess)
                {
                    response.Result = userResponse.Result.Password.Equals(pass);
                }
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult { Message = $"Error al validar la contraseña: {ex.ToString()}" });
            }
            return response;
        }

        public Response<bool> ChangePassword(string newPass, int idUser)
        {
            Response<bool> response = new Response<bool>
            {
                IsSuccess = false,
                Result = false
            };

            try
            {
                Response<User> userResponse = this.GetUserById(idUser);
                userResponse.Result.Password = newPass;
                response.IsSuccess = true;
                Response<User> userResponse2 = this.contract.Update(userResponse.Result);
                response.Result = userResponse2.IsSuccess;
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult { Message = $"Error al cambiar la contraseña: {ex.ToString()}" });
            }
            return response;
        }

        public Response<User> GetUserById(int userId)
        {
            Response<User> response = new Response<User>
            {
                IsSuccess = false,
            };

            try
            {
                response.Result = this.contract
                        .GetAllMatchedIncluding(c => c.Id == userId &&
                                    c.Active == true, c => c.Role)
                        .FirstOrDefault();
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult { Message = $"Error al Consultar Usuario: {ex.ToString()}" });
            }
            return response;

        }
        #endregion
    }
}
