using Application.Abstractions.Messaging;
using Application.UseCases.AccountTypes.Queries;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Accounts.Queries.Get
{
    internal sealed class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, GetAccountByIdResponse>
    {
        private readonly ILogger<GetAccountByIdQueryHandler> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdQueryHandler(
            ILogger<GetAccountByIdQueryHandler> logger, 
            ICustomerRepository customerRepository, 
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Result<GetAccountByIdResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
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

            return Result.Ok(new GetAccountByIdResponse(account));
        }
    }
}
