using CampaignService.Common.Entities;
using CampaignService.Data.Domains.Common;
using CampaignService.UnitOfWorks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CampaignService.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly AppDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            unitOfWork = new UnitOfWork(context);
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            //ExecuteSqlCommand is not used for the current version.
            return context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public virtual IQueryable<T> Table()
        {
            return context.Set<T>().AsQueryable();
        }

        public virtual ICollection<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().Where(match).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().Where(match).ToListAsync();
        }

        public virtual T Add(T entity)
        {
            context.Set<T>().Add(entity);
            unitOfWork.Commit();
            return entity;
        }

        public virtual T Update(T updated)
        {
            var local = context.Set<T>().Local
               .FirstOrDefault(entry => entry.Id.Equals(updated.Id));
            if (local != null)
                context.Entry(local).State = EntityState.Detached;

            context.Set<T>().Attach(updated);
            context.Entry(updated).State = EntityState.Modified;
            unitOfWork.Commit();
            return updated;
        }

        public virtual void Update(T obj, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            context.Set<T>().Attach(obj);

            foreach (var p in propertiesToUpdate)
            {
                context.Entry(obj).Property(p).IsModified = true;
            }

            unitOfWork.Commit();
        }

        public virtual void BulkUpdate(List<T> entities)
        {
            context.BulkUpdate(entities);
        }

        public virtual void BulkInsertOrUpdate(List<T> entities)
        {
            context.BulkInsertOrUpdate(entities);
        }

        public virtual void BulkInsert(List<T> entities)
        {
            context.BulkInsert(entities);
        }

        public virtual void BulkDelete(List<T> entities)
        {
            context.BulkDelete(entities);
        }

        public virtual int Delete(T entity)
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
                context.Entry(local).State = EntityState.Detached;

            context.Set<T>().Remove(entity);
            return unitOfWork.Commit();
        }

        public virtual int Count()
        {
            return context.Set<T>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query;
        }

        public virtual bool Exist(Expression<Func<T, bool>> predicate)
        {
            var exist = context.Set<T>().Where(predicate);
            return exist.Any();
        }

    }
}
