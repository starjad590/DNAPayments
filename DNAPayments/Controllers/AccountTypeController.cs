using Application.Extensions;
using Application.UseCases.AccountTypes.Commands;
using Application.UseCases.AccountTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/accountType/")]
public class AccountTypeController(ISender sender) : ControllerWithMediatR(sender)
{
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAccountTypeAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Send(new GetAccountTypeByIdRequest(id).GetQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.GetErrorResponse();
    }

    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAccountTypesAsync(CancellationToken cancellationToken)
    {
        var result = await Send(new GetAccountTypesQuery(), cancellationToken);
        if (result.IsSuccess)
        {
            if (result.Value.AccountTypes.Count() > 0)
            {
                return Ok(result.Value);
            }
            return NoContent();
        }
        return result.GetErrorResponse();
    }

    [HttpPost]
    public async Task<IActionResult> AddAbilityAsync([FromBody] AddAccountTypeRequest command, CancellationToken cancellationToken)
    {
        var result = await Send(command.GetCommand(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.GetErrorResponse();
    }
}
