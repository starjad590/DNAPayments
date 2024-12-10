using Application.Abstractions.Messaging;

namespace Application.UseCases.Transactions;

public sealed record AddTransactionCommand(Guid CustomerId, string AccountNumber, decimal Amount, string Type) : ICommand { }

public sealed record AddTransactionRequest(Guid CustomerId, string AccountNumber, decimal Amount, string Type)
{
    public AddTransactionCommand GetCommand()
        => new AddTransactionCommand(CustomerId, AccountNumber, Amount, Type);
}