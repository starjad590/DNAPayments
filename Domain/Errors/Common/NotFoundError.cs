using Domain.Errors.BaseErrors;
using Microsoft.Extensions.Logging;

namespace Domain.Errors;

public sealed class NotFoundError : BaseErrors.NotFoundError
{
    private const string Error = "No data could be found for {0} : {1} => {2}";
    public NotFoundError(ILogger logger, string model, string property, object id) : base(string.Format(Error, model, property, id))
    {
        logger.LogError(string.Format(Error, model, property, id));
    }
}
