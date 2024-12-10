using Application.Abstractions.Messaging;

namespace Application.UseCases.Accounts;

public sealed record AddAccountCommand(
    Guid CustomerId,
    Guid AccountTypeId
    ) : ICommand<AddAccountResponse> { }

public sealed record AddAccountResponse(string AccountNumber);

public sealed record AddAccountRequest(Guid CustomerId, Guid AccountTypeId)
{
    public AddAccountCommand GetCommand()
        => new AddAccountCommand(CustomerId, AccountTypeId);
}