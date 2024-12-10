using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Services;

internal sealed class GetNextAccountNumber : IGetNextAccountNumber
{
    private readonly IAccountRepository _accountRepository;

    public GetNextAccountNumber(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    const int DEFAULTNUMBER = 10000000;

    public async Task<string> GetNextNumber()
    {
        var lastId = await _accountRepository.GetLastEntry();

        if (lastId == null)
        {
            return (DEFAULTNUMBER +1).ToString();
        }

        return (int.Parse(lastId) + 1).ToString();
    }
}
