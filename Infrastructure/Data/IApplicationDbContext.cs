using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<AccountType> AccountTypes { get; set; }
    DbSet<Account> Accounts { get; set; }
    DbSet<Transaction> Transactions { get; set; }
}
