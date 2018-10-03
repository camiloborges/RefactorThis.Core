
using RefactorThis.Core.Domain.Interfaces;
using RefactorThis.Core.Models;
using System;

namespace RefactorThis.Core.Infra.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ProductsContext _context;

        public UnitOfWork(ProductsContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
