﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Architecture.Core.GenericRepository
{
    //Does it need a Transaction-method so we can wrap every
    //BusinessLogic in a Transaction if needed
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _entitySet;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            this._entitySet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _entitySet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                            new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return _entitySet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _entitySet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _entitySet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entitySet.Attach(entityToDelete);
            }
            _entitySet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            //attachment not needed as updates occure
            //in-place in BusinessLogic 
            //_entitySet.Attach(entityToUpdate);

            //probably also not needed to set state,
            //as any change in properties already had set modified state
            //_context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
