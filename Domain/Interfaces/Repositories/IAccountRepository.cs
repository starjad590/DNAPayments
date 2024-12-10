using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<string>? GetLastEntry();
    Task<IList<Account>> GetCustomerAccounts(Guid customerId);
    Task<Account>? GetCustomerAccount(Guid customerId, string accountNumber);
}
