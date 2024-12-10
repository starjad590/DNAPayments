using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.UseCases.AccountTypes.Queries;

public sealed record GetAccountTypesQuery : IQuery<GetAccountTypesResponse> { }

public sealed record GetAccountTypesResponse(IEnumerable<AccountType> AccountTypes);

public sealed record GetAccountTypeByIdQuery(Guid Id) : IQuery<GetAccountTypeByIdResponse> { }

public sealed record GetAccountTypeByIdRequest(Guid AccountTypeId)
{
    public GetAccountTypeByIdQuery GetQuery()
        => new GetAccountTypeByIdQuery(AccountTypeId);
}

public sealed record GetAccountTypeByIdResponse(Guid Id, string AccountTypeName);
