using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context) { }

    public async Task<string>? GetLastEntry()
    {
        var account = await _context.Accounts.OrderByDescending(a => a.AccountNumber).FirstOrDefaultAsync();
        return account?.AccountNumber;
    }

    public async Task<IList<Account>> GetCustomerAccounts(Guid customerId)
    {
        return await _context.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
    }

    public async Task<Account>? GetCustomerAccount(Guid customerId, string accountNumber)
    {
        return await _context.Accounts.Where(a => a.CustomerId == customerId && a.AccountNumber == accountNumber).FirstOrDefaultAsync();
    }
}