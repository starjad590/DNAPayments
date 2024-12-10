using Application.Abstractions.Messaging;

namespace Application.UseCases.AccountTypes.Commands;

public sealed record AddAccountTypeCommand(string AccountTypeName) : ICommand<AddAccountTypeResponse> { }

public sealed record AddAccountTypeRequest(string AccountTypeName)
{
    public AddAccountTypeCommand GetCommand()
        => new AddAccountTypeCommand(AccountTypeName);
}

public sealed record AddAccountTypeResponse(Guid AccountTypeId);
