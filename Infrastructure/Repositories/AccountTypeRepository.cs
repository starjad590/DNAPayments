using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class AccountTypeRepository : GenericRepository<AccountType>, IAccountTypeRepository
{
    public AccountTypeRepository(ApplicationDbContext context) : base(context) { }

    public async Task<AccountType?> GetWithName(string name)
    {
        return await _context.AccountTypes.Where(x => x.AccountTypeName == name).FirstOrDefaultAsync();
    }
}
