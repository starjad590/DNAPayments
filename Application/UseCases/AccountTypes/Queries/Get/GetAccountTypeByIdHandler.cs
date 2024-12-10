using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountTypes.Queries;

internal sealed class GetAccountTypeByIdHandler : IQueryHandler<GetAccountTypeByIdQuery, GetAccountTypeByIdResponse>
{
    private readonly ILogger<GetAccountTypeByIdHandler> _logger;
    private readonly IAccountTypeRepository _accountTypeRepository;

    public GetAccountTypeByIdHandler(
        ILogger<GetAccountTypeByIdHandler> logger, 
        IAccountTypeRepository accountTypeRepository)
    {
        _logger = logger;
        _accountTypeRepository = accountTypeRepository;
    }

    public async Task<Result<GetAccountTypeByIdResponse>> Handle(GetAccountTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var accountType = await _accountTypeRepository.GetByIdAsync(request.Id);

        if (accountType == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(AccountType), nameof(request.Id), request.Id));
        }

        return new GetAccountTypeByIdResponse(accountType.Id, accountType.AccountTypeName);
    }
}
