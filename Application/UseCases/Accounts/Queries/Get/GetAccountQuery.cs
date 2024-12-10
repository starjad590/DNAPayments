using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.UseCases.Accounts;

public sealed record GetAccountByIdQuery(Guid CustomerId, string AccountNumber) : IQuery<GetAccountByIdResponse> { }

public sealed record GetAccountRequest(Guid CustomerId, string AccountNumber)
{
    public GetAccountByIdQuery GetQuery()
        => new GetAccountByIdQuery(CustomerId, AccountNumber);
}

public sealed record GetAccountByIdResponse(Account Account);

public sealed record GetAccountsQuery(Guid CustomerId) : IQuery<GetAccountsResponse> { }

public sealed record GetAccountsRequest(Guid CustomerId)
{
    public GetAccountsQuery GetQuery() 
        =>new GetAccountsQuery(CustomerId);
}

public sealed record GetAccountsResponse(IEnumerable<Account> Accounts);