using Application.Extensions;
using Application.UseCases.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/account/")]
public class AccountController(ISender sender) : ControllerWithMediatR(sender)
{
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAccountAsync([FromQuery] GetAccountRequest query, CancellationToken cancellationToken)
    {
        var result = await Send(query.GetQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.GetErrorResponse();
    }

    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAccountsAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var result = await Send(new GetAccountsRequest(customerId).GetQuery(), cancellationToken);
        if (result.IsSuccess)
        {
            if (result.Value.Accounts.Count() > 0)
            {
                return Ok(result.Value);
            }
            return NoContent();
        }
        return result.GetErrorResponse();
    }

    [HttpPost]
    public async Task<IActionResult> AddAccountAsync([FromBody] AddAccountRequest command, CancellationToken cancellationToken)
    {
        var result = await Send(command.GetCommand(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.GetErrorResponse();
    }

    [HttpPatch]
    [Route("Freeze")]
    public async Task<IActionResult> FreezeAccountAsync(FreezeAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await Send(request.GetCommand(), cancellationToken);
        return result.IsSuccess ? NoContent() : result.GetErrorResponse();
    }
}
