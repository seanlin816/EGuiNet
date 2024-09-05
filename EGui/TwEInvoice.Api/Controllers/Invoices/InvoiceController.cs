using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TwEInvoice.Api.Controllers.Invoices;

[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    public async Task<IActionResult> IssueInvoice(IssueInvoiceRequest request)
    {
        return Ok();
    }
}