using Application.Abstractions.Messaging;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountTypes.Queries;

internal sealed class GetAccountTypesHandler : IQueryHandler<GetAccountTypesQuery, GetAccountTypesResponse>
{
    private readonly ILogger<GetAccountTypesHandler> _logger;
    private readonly IAccountTypeRepository _accountTypeRepository;
    public GetAccountTypesHandler(
        ILogger<GetAccountTypesHandler> logger, 
        IAccountTypeRepository accountTypeRepository)
    {
        _logger = logger;
        _accountTypeRepository = accountTypeRepository;
    }

    public async Task<Result<GetAccountTypesResponse>> Handle(GetAccountTypesQuery request, CancellationToken cancellationToken)
    {
        var accountTypes = await _accountTypeRepository.GetAllAsync();
        return Result.Ok(new GetAccountTypesResponse(accountTypes));
    }
}
