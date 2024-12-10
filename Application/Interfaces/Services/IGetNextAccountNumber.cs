namespace Application.Interfaces.Services;

public interface IGetNextAccountNumber
{
    Task<string> GetNextNumber();
}
