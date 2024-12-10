using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountTypes.Commands;

internal sealed class AddAccountTypeCommandHandler : ICommandHandler<AddAccountTypeCommand, AddAccountTypeResponse>
{
    private readonly ILogger<AddAccountTypeCommandHandler> _logger;
    private readonly IAccountTypeRepository _accountTypeRepository;
    public AddAccountTypeCommandHandler(
        ILogger<AddAccountTypeCommandHandler> logger, 
        IAccountTypeRepository accountTypeRepository)
    {
        _logger = logger;
        _accountTypeRepository = accountTypeRepository;
    }

    public async Task<Result<AddAccountTypeResponse>> Handle(AddAccountTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accountTypeRepository.GetWithName(request.AccountTypeName);

        if (entity is not null)
        {
            return Result.Fail(new AlreadyExistsError(_logger, nameof(AccountType), nameof(request.AccountTypeName), request.AccountTypeName));
        }

        var accountType = AccountType.Create(request.AccountTypeName);

        var newEntityId = await _accountTypeRepository.AddAsync(accountType);

        return Result.Ok(new AddAccountTypeResponse(newEntityId));
    }
}
