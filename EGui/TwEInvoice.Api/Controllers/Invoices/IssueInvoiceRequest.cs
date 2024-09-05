namespace TwEInvoice.Api.Controllers.Invoices;

public sealed record IssueInvoiceRequest(
    string SellerTaxId,
    string? BuyerTaxId,
    string? AcctPeriod,
    string IssueDate,
    string Currency,
    decimal TotalPrice,
    string CarrierType,
    List<IssueInvoiceRequestItem> Items
    );

public record IssueInvoiceRequestItem(
    string Description, 
    int Quantity,
    decimal UnitPrice,
    decimal Price
    );