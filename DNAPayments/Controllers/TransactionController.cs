using Application.Extensions;
using Application.UseCases.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/transaction/")]
public class TransactionController(ISender sender) : ControllerWithMediatR(sender)
{
    [HttpPost]
    public async Task<IActionResult> AddTransactionAsync([FromBody] AddTransactionRequest command, CancellationToken cancellationToken)
    {
        var result = await Send(command.GetCommand(), cancellationToken);
        return result.IsSuccess ? NoContent() : result.GetErrorResponse();
    }
}
