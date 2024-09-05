using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwEInvoice.Application.Invoices.CreateInvoiceBook;
using TwEInvoice.Application.Invoices.IssueInvoice;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;
using TwEInvoice.Domain.Invoices.VO;

namespace TwEInvoice.Api.Controllers.Invoices;

[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    public readonly ISender _sender;

    public InvoiceController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("issue")]
    public async Task<IActionResult> IssueInvoice(IssueInvoiceRequest request, CancellationToken cancellationToken)
    {
        var command = new IssueInvoiceCommand(
            Period: (request.AcctPeriod == null)
                ? DateTimeUtil.GetCurrentAcctPeriod()
                : AccountingPeriod.Parse(request.AcctPeriod),
            Buyer: new Buyer(Name: null, TaxId: request.BuyerTaxId, Address: null),
            Seller: new Seller(Name: null, TaxId: request.SellerTaxId, Address: null)
        );
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction("CreateInvoiceBook", new { id = result.Value }, result.Value);
    }

    [HttpPost]
    [Route("book/new")]
    public async Task<IActionResult> CreateInvoiceBook(CreateInvoiceBookRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateInvoiceBookCommand(
            AccountPeriod: AccountingPeriod.Parse(request.AccountPeriod),
            Track: new InvoiceTrack(request.Track),
            SellerTaxId: new SellerTaxId(request.SellerTaxId),
            StartNumber: request.StartNumber,
            EndNumber: request.EndNumber
        );
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction("CreateInvoiceBook", new { id = result.Value }, result.Value);
    }
}