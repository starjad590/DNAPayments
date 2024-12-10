using Application.Extensions;
using Application.UseCases.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/customer/")]
public class CustomerController(ISender sender) : ControllerWithMediatR(sender)
{
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await Send(new GetCustomerRequest(id).GetQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.GetErrorResponse();
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomerAsync([FromBody] AddCustomerRequest command, CancellationToken cancellationToken)
    {
        var result = await Send(command.GetCommand(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.GetErrorResponse();
    }
}
