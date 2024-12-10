using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.UseCases.Customers;

public sealed record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdResponse> { }

public sealed record GetCustomerRequest(Guid Id)
{
    public GetCustomerByIdQuery GetQuery()
        => new GetCustomerByIdQuery(Id);
}

public sealed record GetCustomerByIdResponse(Customer Customer);