using David.OpheliaTest.Common.Helpers;
using David.OpheliaTest.Common.Models;
using David.OpheliaTest.DataAccessLayer.Contracts;
using David.OpheliaTest.DataAccessLayer.Helpers;
using David.OpheliaTest.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace David.OpheliaTest.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDataContext context;

        public GenericRepository(ApplicationDataContext context)
        {
            this.context = context;
        }


        #region Regular Members

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        public IList<T> GetAllMatched(Expression<Func<T, bool>> match)
        {
            return this.context.Set<T>().Where(match).ToList();
        }
        public IQueryable<T> GetAllMatchedIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = this.context.Set<T>().Where(match).AsNoTracking();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }
        public IQueryable<T> IncludeAll(IQueryable<T> queryable)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual && properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null)
                {
                    queryable = queryable.Include(property.Name);
                }
            }
            return queryable;
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable;
        }
        public IQueryable<T> GetByIdAndIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = (context.Set<T>().AsNoTracking().Where(c => c.Id == id));
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable;

        }
        public T GetById(int id)
        {
            return this.context.Set<T>()
                .AsNoTracking().FirstOrDefault(e => e.Id == id);

        }
        public T Find(Expression<Func<T, bool>> match)
        {
            return this.context.Set<T>()
                    .AsNoTracking().SingleOrDefault(match);

        }

        public IList<T> GetAllPaged(int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = this.context.Set<T>().Count();
            return this.context.Set<T>().Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }
        public IList<T> GetAllPagedMatched(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> match)
        {
            totalCount = this.context.Set<T>().Count();
            return this.context.Set<T>().Skip(pageSize * pageIndex).Take(pageSize).Where(match).ToList();
        }
        public IList<T> GetAllPagedMatchedIncluding(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties)
        {
            totalCount = this.context.Set<T>().Count();
            IQueryable<T> queryable = this.context.Set<T>().Skip(pageSize * pageIndex).Take(pageSize).Where(match);
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable.ToList();
        }

        public IList<T> GetAllPagedMatcheds(int pageIndex, int pageSize, out int totalCount, List<PaginatorRequestQuery> match)
        {
            List<Expression> expressions = new List<Expression>();
            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");

            foreach (var item in match)
            {
                // Key as Name Property
                Expression nameProperty = Expression.Property(argParam, item.ProperyName);

                //Value as Key Value
                var val = Expression.Constant(item.ProperyName);

                var parameterExp = Expression.Parameter(typeof(T), "type");

                var propertyExp = Expression.Property(parameterExp, item.ProperyName);


                string propertyType = nameProperty.Type.FullName;
                if (propertyType == "System.String")
                {
                    MethodInfo method = typeof(string).GetMethod(item.Sentence, new[] { typeof(string) });
                    var someValue1 = Expression.Constant(item.ProperyValue, typeof(string));
                    var containsMethodExp1 = Expression.Call(propertyExp, method, someValue1);
                    expressions.Add(containsMethodExp1);
                }
                else
                {
                    MethodInfo method = typeof(int).GetMethod(item.Sentence, new[] { typeof(int) });
                    var someValue1 = Expression.Constant(Convert.ToInt32(item.ProperyValue), typeof(int));
                    var containsMethodExp1 = Expression.Call(propertyExp, method, someValue1);
                    expressions.Add(containsMethodExp1);
                }


            }


            Expression<Func<T, bool>> responseExpression = Utils.And<T>(expressions);

            IList<T> responseMached = this.GetAllPagedMatched(pageIndex, pageSize, out totalCount, responseExpression);
            totalCount = responseMached.Count;
            return responseMached;


        }

        public IList<T> GetAllPagedMatchedsIncluding(int pageIndex, int pageSize, out int totalCount,
            List<PaginatorRequestQuery> match, params Expression<Func<T, object>>[] includeProperties)
        {
            List<Expression> expressions = new List<Expression>();
            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");

            foreach (var item in match)
            {
                // Key as Name Property
                Expression nameProperty = Expression.Property(argParam, item.ProperyName);

                //Value as Key Value
                var val = Expression.Constant(item.ProperyName);

                var parameterExp = Expression.Parameter(typeof(T), "type");

                var propertyExp = Expression.Property(parameterExp, item.ProperyName);


                string propertyType = nameProperty.Type.FullName;
                if (propertyType == "System.String")
                {
                    MethodInfo method = typeof(string).GetMethod(item.Sentence, new[] { typeof(string) });
                    var someValue1 = Expression.Constant(item.ProperyValue, typeof(string));
                    var containsMethodExp1 = Expression.Call(propertyExp, method, someValue1);
                    expressions.Add(containsMethodExp1);
                }
                else
                {
                    MethodInfo method = typeof(int).GetMethod(item.Sentence, new[] { typeof(int) });
                    var someValue1 = Expression.Constant(Convert.ToInt32(item.ProperyValue), typeof(int));
                    var containsMethodExp1 = Expression.Call(propertyExp, method, someValue1);
                    expressions.Add(containsMethodExp1);
                }

            }


            Expression<Func<T, bool>> responseExpression = Utils.And<T>(expressions);

            IList<T> responseMached = this.GetAllPagedMatchedIncluding(pageIndex, pageSize, out totalCount, responseExpression, includeProperties);
            totalCount = responseMached.Count;
            return responseMached;


        }

        public IList<T> GetAllPagedIncluding(int pageIndex, int pageSize, out int totalCount,
             params Expression<Func<T, object>>[] includeProperties)
        {


            totalCount = this.context.Set<T>().Count();
            IQueryable<T> queryable = this.context.Set<T>().Skip(pageSize * pageIndex).Take(pageSize);
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable.ToList();


        }
        public int Count()
        {
            return this.context.Set<T>().Count();
        }
        public Response<T> Create(T entity)
        {
            try
            {
                entity.LastUpdate = DateTime.Now;
                entity.Active = true;
                this.context.Set<T>().Add(entity);
                this.context.SaveChanges();
                return ResponseHelper<T>.SuccessResponse(
                    string.Format("Create: {0} with ID:{1} ", entity.GetType().FullName, entity.Id), entity);

            }
            catch (Exception ex)
            {
                return ResponseHelper<T>.ExceptionDatabase(ex, "Insertar", entity.GetType().FullName, entity);

            }
        }


        public Response<T> Delete(T entity)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Set<T>().Remove(entity);
            try
            {
                this.context.SaveChanges();
                return ResponseHelper<T>.SuccessResponse(
                  string.Format("Registro Eliminado: {0}", entity.GetType().FullName), entity);
            }
            catch (Exception ex)
            {
                return ResponseHelper<T>.ExceptionDatabase(ex, "ELIMINAR", entity.GetType().FullName, entity);

            }

        }

        public Response<T> Update(T entity)
        {
            var entry = context.Set<T>().Update(entity);
            try
            {
                entity.Active = true;
                entity.LastUpdate = DateTime.Now;
                entry.State = EntityState.Modified;
                this.context.SaveChanges();
                return ResponseHelper<T>.SuccessResponse(
                string.Format("Registro Actualizado: {0}", entity.GetType().FullName), entity);
            }
            catch (Exception ex)
            {
                return ResponseHelper<T>.ExceptionDatabase(ex, "Actualizar", entity.GetType().FullName, entity);
            }
        }

        #endregion


        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync();
            //return null;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await this.context.Set<T>().Where(match).ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await this.context.Set<T>().FindAsync(id);

        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await this.context.Set<T>().SingleOrDefaultAsync(match);
        }

        public async Task<int> CountAsync()
        {
            return await this.context.Set<T>().CountAsync();
        }

        public async Task<bool> CreateAsync(T entity, bool saveChanges = false)
        {
            var rtn = await this.context.Set<T>().AddAsync(entity);
            if (saveChanges)
            {
                return await SaveAllAsync();

            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id, bool saveChanges = false)
        {
            this.context.Set<T>().Remove(GetById(id));
            if (saveChanges)
            {
                try
                {
                    await this.context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
        }


        public async Task<bool> DeleteAsync(T entity, bool saveChanges = false)
        {
            this.context.Set<T>().Remove(entity);
            if (saveChanges)
            {
                try
                {
                    await this.context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
        }


        public async Task<bool> UpdateAsync(T entity, bool saveChanges = false)
        {
            this.context.Set<T>().Update(entity);

            if (saveChanges)
            {
                await SaveAllAsync();
            }
            return false;
        }


        public async Task<bool> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);

            return await SaveAllAsync();


        }

        public async Task<bool> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);

            return await SaveAllAsync();


        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<T>().AnyAsync(e => e.Id == id);
            //return await this.context.Set<T>().AnyAsync(e => e.ToString() == (Convert.ToString(id)));

        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                await this.context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IList<T>> GetByAllIdAsync(int id)
        {
            return (await this.context.Set<T>()
                .AsNoTracking().ToListAsync());

            //return (await this.context.Set<T>()
            //    .AsNoTracking().Where(c => c.Id == id).ToListAsync());
        }

        public IList<T> GetAllMatched(RequestQuery machetds)
        {
            List<Expression> expressions = new List<Expression>();
            ParameterExpression argParam = Expression.Parameter(typeof(T));

            foreach (var item in machetds.Query)
            {
                // Key as Name Property
                Expression nameProperty = Expression.Property(argParam, item.ProperyName);

                //Value as Key Value
                var val = Expression.Constant(item.ProperyName);

                var parameterExp = Expression.Parameter(typeof(T), "type");

                var propertyExp = Expression.Property(parameterExp, item.ProperyName);




                if (Int32.TryParse(item.ProperyValue, out int result))
                {
                    MethodInfo method = typeof(int).GetMethod(item.Sentence, new[] { typeof(int) });
                    var someValue1 = Expression.Constant(Convert.ToInt32(item.ProperyValue), typeof(int));
                    var containsMethodExp1 = Expression.Call(propertyExp, method, someValue1);
                    expressions.Add(containsMethodExp1);
                }
                else
                {
                    MethodInfo method = typeof(string).GetMethod(item.Sentence, new[] { typeof(string) });
                    var someValue1 = Expression.Constant(item.ProperyValue, typeof(string));
                    var containsMethodExp1 = Expression.Call(propertyExp, method, someValue1);
                    expressions.Add(containsMethodExp1);
                }

            }


            Expression<Func<T, bool>> responseExpression = Utils.And<T>(expressions);

            IList<T> responseMached = this.GetAllMatched(responseExpression);
            return responseMached;
        }
    }
}
