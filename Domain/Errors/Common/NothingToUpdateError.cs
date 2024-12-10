using Domain.Errors.BaseErrors;
using Microsoft.Extensions.Logging;

namespace Domain.Errors;

public sealed class NothingToUpdateError : BadRequestError
{
    private const string Error = "{0} no values to update : {1} => {2}";
    public NothingToUpdateError(ILogger logger, string model, string property, object id) : base(string.Format(Error, model, property, id))
    {
        logger.LogError(string.Format(Error, model, property, id));
    }
}
