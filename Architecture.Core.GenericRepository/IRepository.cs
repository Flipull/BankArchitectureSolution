using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Architecture.Core.GenericRepository
{
    public enum SortDirection { Ascending, Descending };
    //need Generics and Interfaces for keyless repo's?
    //for surrogate-key repo's?
    //does this already work for compound-key repo's?
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                            Expression<Func<TEntity, object>>
                                        orderBy = null,
                                int? skip = null,
                                int? take = null,
                                string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void SaveChanges();
    }
}