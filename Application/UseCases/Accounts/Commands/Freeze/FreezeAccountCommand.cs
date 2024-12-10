using Application.Abstractions.Messaging;

namespace Application.UseCases.Accounts;

public sealed record FreezeAccountCommand(Guid CustomerId, string AccountNumber) : ICommand { }

public sealed record FreezeAccountRequest(Guid CustomerId, string AccountNumber)
{
    public FreezeAccountCommand GetCommand()
        => new FreezeAccountCommand(CustomerId, AccountNumber);
}