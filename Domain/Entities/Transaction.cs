using Domain.Abstractions;
using Domain.Extensions;
using System.Numerics;

namespace Domain.Entities;

public sealed class Transaction : Entity<Guid>
{
    public Guid AccountId { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public int AmountInPence { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Account Account { get; set; } = null!;

    private Transaction()
    {
    }

    private Transaction(Guid accountId, string transactionType, decimal amount)
    {
        AccountId = accountId;
        TransactionType = transactionType;
        AmountInPence = amount.PoundsToPence();
        TransactionDate = DateTime.Now;
    }

    public static Transaction Create(Guid accountId, string transactionType, decimal amount) 
        => new Transaction(accountId, transactionType, amount);
}
