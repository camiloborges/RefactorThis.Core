using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Infrastructure.Data
{
    public abstract class EfRepository : IRepository
    {
        private readonly ProductsContext _dbContext;

        public EfRepository(ProductsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById<T>(Guid id) where T : BaseEntity
        {
         var result =  _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
            if (result is null)
                throw new KeyNotFoundException();
            return result;
        }

        public List<T> ListByName<T>(string name) where T : BaseEntity
        {
            return _dbContext.Set<T>().Where(p => p.Name.ToUpperInvariant().Contains(name.ToUpperInvariant(),StringComparison.InvariantCulture)).ToList();
        }
        public List<T> List<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Add<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
