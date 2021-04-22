using David.OpheliaTest.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace David.OpheliaTest.DataAccessLayer.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> IncludeAll(IQueryable<T> queryable);
        IQueryable<T> GetAll();
        Task<IList<T>> GetAllAsync();

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);

        IList<T> GetAllMatched(Expression<Func<T, bool>> match);
        IList<T> GetAllMatched(RequestQuery machetds);
        IQueryable<T> GetAllMatchedIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties);
        IList<T> GetAllPagedMatchedIncluding(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties);

        IList<T> GetAllPagedMatchedsIncluding(int pageIndex, int pageSize, out int totalCount,
           List<PaginatorRequestQuery> match, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetByIdAndIncluding(int id, params Expression<Func<T, object>>[] includeProperties);
        T GetById(int id);

        T Find(Expression<Func<T, bool>> match);


        IList<T> GetAllPagedMatched(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> match);
        IList<T> GetAllPagedMatcheds(int pageIndex, int pageSize, out int totalCount, List<PaginatorRequestQuery> machetds);

        IList<T> GetAllPaged(int pageIndex, int pageSize, out int totalCount);

        int Count();

        Response<T> Create(T entity);



        Response<T> Delete(T entity);

        Response<T> Update(T entity);

        Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetByAllIdAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<int> CountAsync();
        Task<bool> DeleteAsync(int id, bool saveChanges = false);
        Task<bool> DeleteAsync(T entity, bool saveChanges = false);
        Task<bool> UpdateAsync(T entity, bool saveChanges = false);
        IList<T> GetAllPagedIncluding(int pageIndex, int pageSize, out int totalCount, params Expression<Func<T, object>>[] includeProperties);
    }
}
