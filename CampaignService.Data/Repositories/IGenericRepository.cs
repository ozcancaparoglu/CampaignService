using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CampaignService.Repositories
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        T Add(T entity);
        void BulkDelete(List<T> entities);
        void BulkInsert(List<T> entities);
        void BulkInsertOrUpdate(List<T> entities);
        void BulkUpdate(List<T> entities);
        int Count();
        Task<int> CountAsync();
        int Delete(T entity);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        bool Exist(Expression<Func<T, bool>> predicate);
        IQueryable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = "", int? page = null, int? pageSize = null);
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Table();
        T Update(T updated);
        void Update(T obj, params Expression<Func<T, object>>[] propertiesToUpdate);
    }
}