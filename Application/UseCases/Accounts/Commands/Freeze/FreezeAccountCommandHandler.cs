using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Accounts;

internal sealed class FreezeAccountCommandHandler : ICommandHandler<FreezeAccountCommand>
{
    private ILogger<FreezeAccountCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;

    public FreezeAccountCommandHandler(
        ILogger<FreezeAccountCommandHandler> logger, 
        ICustomerRepository customerRepository, 
        IAccountRepository accountRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Result> Handle(FreezeAccountCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(Customer), nameof(request.CustomerId), request.CustomerId));
        }

        var account = await _accountRepository.GetCustomerAccount(customer.Id, request.AccountNumber);

        if (account == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(Account), nameof(request.AccountNumber), request.AccountNumber));
        }

        account.DisabledAt = DateTime.Now;

        await _accountRepository.UpdateAsync(account);

        return Result.Ok();
    }
}
