using Application.Abstractions.Messaging;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Accounts;

internal sealed class AddAccountCommandHandler : ICommandHandler<AddAccountCommand, AddAccountResponse>
{
    private readonly ILogger<AddAccountCommandHandler> _logger;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountTypeRepository _accountTypeRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IGetNextAccountNumber _getNextAccountNumber;

    public AddAccountCommandHandler(
        ILogger<AddAccountCommandHandler> logger,
        IAccountRepository accountRepository,
        IAccountTypeRepository accountTypeRepository,
        ICustomerRepository customerRepository,
        IGetNextAccountNumber getNextAccountNumber)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _accountTypeRepository = accountTypeRepository;
        _customerRepository = customerRepository;
        _getNextAccountNumber = getNextAccountNumber;
    }

    public async Task<Result<AddAccountResponse>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(Customer), nameof(request.CustomerId), request.CustomerId));
        }

        var accountType = await _accountTypeRepository.GetByIdAsync(request.AccountTypeId);

        if (accountType == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(AccountType), nameof(request.AccountTypeId), request.AccountTypeId));
        }

        var nextAccountNumber = await _getNextAccountNumber.GetNextNumber();
        var account = Account.Create(nextAccountNumber, request.CustomerId, request.AccountTypeId);

        var newEntityId = await _accountRepository.AddAsync(account);

        return Result.Ok(new AddAccountResponse(account.AccountNumber));
    }
}
