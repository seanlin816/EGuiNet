namespace TwEInvoice.Domain.Invoices.InvoiceNumbers;

public record InvoiceNumber(string TrackId, int SerialNumber)
{
    public override string ToString()
    {
        return $"{TrackId}{SerialNumber:00000000}";
    }
}