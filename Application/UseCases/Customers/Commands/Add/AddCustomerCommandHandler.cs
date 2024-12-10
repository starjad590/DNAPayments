using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Customers;
internal sealed class AddCustomerCommandHandler : ICommandHandler<AddCustomerCommand, AddCustomerResponse>
{
    private readonly ILogger<AddCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerCommandHandler(
        ILogger<AddCustomerCommandHandler> logger, 
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<Result<AddCustomerResponse>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.GetWithEmailAsync(request.Email);

        if (entity is not null)
        {
            return Result.Fail(new AlreadyExistsError(_logger, nameof(Customer), nameof(request.Email), request.Email));
        }

        var customer = Customer.Create(
                request.FirstName, 
                request.LastName, 
                request.Email, 
                request.PhoneNumber, 
                request.Address
            );

        var newEntityId = await _customerRepository.AddAsync(customer);

        return Result.Ok(new AddCustomerResponse(newEntityId));
    }
}
