using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Account : Entity<Guid>
{
    public string AccountNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; } 
    public Guid AccountTypeId { get; set; }
    public int BalanceInPence { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DisabledAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Navigation properties
    public ICollection<Transaction>? Transactions { get; set; }

    private Account()
    {
            
    }

    private Account(string accountNumber, Guid customerId, Guid accountTypeId)
    {
        AccountNumber = accountNumber;
        CustomerId = customerId;
        AccountTypeId = accountTypeId;
        CreatedAt = DateTime.Now;
    }

    public static Account Create(string accountNumber, Guid customerId, Guid accountTypeId)
        => new Account(accountNumber, customerId, accountTypeId);
}
