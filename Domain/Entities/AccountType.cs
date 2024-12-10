using Domain.Abstractions;

namespace Domain.Entities;

public sealed class AccountType : Entity<Guid>
{
    public string AccountTypeName { get; set; } = string.Empty;


    private AccountType()
    {
            
    }

    private AccountType(string accountTypeName)
    {
        AccountTypeName = accountTypeName;
    }

    public static AccountType Create(string accountTypeName)
        => new AccountType(accountTypeName);
}
