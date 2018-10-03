using System;
using System.Linq;

namespace RefactorThis.Core.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity item);

        TEntity GetById(Guid id);

        IQueryable<TEntity> GetAll();

        void Update(TEntity item);

        void Remove(Guid id);

        int SaveChanges();
    }
}