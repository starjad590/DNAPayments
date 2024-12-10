using Domain.Errors.BaseErrors;
using Microsoft.Extensions.Logging;

namespace Domain.Errors;

public sealed class AlreadyExistsError : BadRequestError
{
    private const string Error = "{0} already exists : {1} => {2}";
    public AlreadyExistsError(ILogger logger, string model, string property, object id) : base(string.Format(Error, model, property, id))
    {
        logger.LogError(string.Format(Error, model, property, id));
    }
}
