using David.OpheliaTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.BusinessLayer.Contracts
{
    public interface ISaleDetailService : IService<SaleDetail>
    {
        bool PostAll(List<SaleDetail> saleDetails);
    }
}
