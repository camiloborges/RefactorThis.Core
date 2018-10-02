using Equinox.Domain.Models;

namespace RefactorThis.Core.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}