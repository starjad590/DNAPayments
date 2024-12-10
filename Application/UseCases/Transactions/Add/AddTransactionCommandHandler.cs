using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Errors.Common;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Transactions;

internal sealed class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand>
{
    private readonly ILogger<AddTransactionCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public AddTransactionCommandHandler(
        ILogger<AddTransactionCommandHandler> logger, 
        ICustomerRepository customerRepository, 
        IAccountRepository accountRepository, 
        ITransactionRepository transactionRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<Result> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
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

        if (account.DisabledAt is not null)
        {
            return Result.Fail(new AccountDisabledError(_logger, nameof(Account), nameof(request.AccountNumber), request.AccountNumber));
        }

        var transaction = Transaction.Create(account.Id, request.Type, request.Amount);

        return Result.Ok();
    }
}
