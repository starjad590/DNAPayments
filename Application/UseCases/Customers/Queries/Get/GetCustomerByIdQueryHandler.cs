using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Customers;

internal sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    private readonly ILogger<GetCustomerByIdQueryHandler> _logger;  
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(
        ILogger<GetCustomerByIdQueryHandler> logger, 
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            return Result.Fail(new NotFoundError(_logger, nameof(Customer), nameof(request.Id), request.Id));
        }

        return new GetCustomerByIdResponse(customer!);
    }
}
