using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Accounts;

internal class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, GetAccountsResponse>
{
    private readonly ILogger<GetAccountsQueryHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;

    public GetAccountsQueryHandler(
        ILogger<GetAccountsQueryHandler> logger,
        ICustomerRepository customerRepository,
        IAccountRepository accountRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Result<GetAccountsResponse>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(Customer), nameof(request.CustomerId), request.CustomerId));
        }

        var accounts = await _accountRepository.GetCustomerAccounts(customer.Id);
        return Result.Ok(new GetAccountsResponse(accounts));
    }
}
