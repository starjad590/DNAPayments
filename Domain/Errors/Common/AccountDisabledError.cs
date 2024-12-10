using Domain.Errors.BaseErrors;
using Microsoft.Extensions.Logging;

namespace Domain.Errors.Common;

public sealed class AccountDisabledError : BadRequestError
{
    private const string Error = "Customer Account is Disabled : {1} => {2}";
    public AccountDisabledError(ILogger logger, string model, string property, object id) : base(string.Format(Error, model, property, id))
    {
        logger.LogError(string.Format(Error, model, property, id));
    }
}