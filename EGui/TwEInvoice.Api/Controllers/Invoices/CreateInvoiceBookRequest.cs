namespace TwEInvoice.Api.Controllers.Invoices;

public record CreateInvoiceBookRequest(
    string AccountPeriod,
    string SellerTaxId,
    string Track,
    int StartNumber,
    int EndNumber);