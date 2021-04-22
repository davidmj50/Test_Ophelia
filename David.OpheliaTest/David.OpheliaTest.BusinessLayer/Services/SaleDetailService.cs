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
    public class SaleDetailService : ISaleDetailService
    {
        #region Properties
        private readonly ISaleDetailRepository contract;
        private readonly ISaleService saleContract;
        private readonly IProductService productContract;
        private readonly IWalletService walletContract;
        #endregion

        #region Constructor
        public SaleDetailService(ISaleDetailRepository contract, 
            ISaleService saleContract, IProductService productContract, IWalletService walletContract)
        {
            this.contract = contract;
            this.saleContract = saleContract;
            this.productContract = productContract;
            this.walletContract = walletContract;
        }
        #endregion

        #region Implement Methods
        public Response<PaginatorResponse<SaleDetail>> GetAllPaged(PaginatorRequest model)
        {
            Response<PaginatorResponse<SaleDetail>> response = new Response<PaginatorResponse<SaleDetail>>
            {
                IsSuccess = false,
                Result = new PaginatorResponse<SaleDetail>()
            };
            if (model.PageSize == 0)
            {
                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = "PageSize no puede ser 0" } };
            }

            IList<SaleDetail> paginatorResponse = contract.GetAllPaged(model.PageIndex, model.PageSize, out int totalCount);
            response.Result.TotalRows = totalCount;
            response.IsSuccess = true;
            response.Result.Results = paginatorResponse;
            return response;
        }
        public Response<IList<SaleDetail>> GetAllMacheds(RequestQuery model)
        {
            Response<IList<SaleDetail>> response = new Response<IList<SaleDetail>>
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
        public Response<SaleDetail> Delete(SaleDetail entity)
        {
            if (entity == null)
            {
                return ResponseHelper<SaleDetail>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<SaleDetail> Get()
        {
            return contract.GetAll().ToList();
        }
        public SaleDetail Get(int id)
        {
            return contract.GetById(id);
        }
        public Response<SaleDetail> Post(SaleDetail entity)
        {
            if (entity == null)
            {
                return ResponseHelper<SaleDetail>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<SaleDetail> Put(SaleDetail entity)
        {
            if (entity == null)
            {
                return ResponseHelper<SaleDetail>.ErrorResponse(new Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }

        public bool PostAll(List<SaleDetail> saleDetails)
        {
            try
            {
                foreach (var saleDetail in saleDetails)
                {
                    this.Post(saleDetail);
                }
                Sale sale = saleDetails[0].Sale;
                User user = sale.User;
                int wholeAmount = 0;
                foreach (var item in saleDetails)
                {
                    wholeAmount += (int)item.TotalSale;
                    Product product = item.Product;
                    int stock = product.Stock - item.Amount;
                    product.Stock = stock;
                    this.productContract.Put(product);
                }
                Wallet wallet = walletContract.GetPointsByIdUser(user.Id);
                wallet.Points = wallet.Points - wholeAmount;
                walletContract.Put(wallet);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
    }
}
