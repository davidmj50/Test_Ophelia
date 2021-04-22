using David.OpheliaTest.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.BusinessLayer.Contracts
{
    public interface IService<T> where T : class
    {
        List<T> Get();
        T Get(int id);
        Response<T> Put(T entity);
        Response<T> Post(T entity);
        Response<T> Delete(T entity);
        Response<PaginatorResponse<T>> GetAllPaged(PaginatorRequest model);
        Response<IList<T>> GetAllMacheds(RequestQuery model);
    }
}
