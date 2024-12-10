using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAccountTypeRepository : IGenericRepository<AccountType>
{
    Task<AccountType?> GetWithName(string name);
}