using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Errors.BaseErrors;

public class BadRequestError(string error) : Error(error), IError
{
    public static string Title = "Bad Request";

    public static ObjectResult Problem(string error) =>
        new ObjectResult(new ProblemDetails()
        {
            Title = Title,
            Status = 400,
            Detail = error
        });
}
