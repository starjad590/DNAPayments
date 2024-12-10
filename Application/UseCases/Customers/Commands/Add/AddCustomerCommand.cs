using Application.Abstractions.Messaging;

namespace Application.UseCases.Customers;

public sealed record AddCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address
    ) : ICommand<AddCustomerResponse> { }

public sealed record AddCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address)
{
    public AddCustomerCommand GetCommand()
        => new AddCustomerCommand(FirstName,LastName,Email,PhoneNumber,Address);
}

public sealed record AddCustomerResponse(Guid CustomerId);