using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetWithEmailAsync(string email);
}
