namespace TwEInvoice.Domain.Invoices.InvoiceNumbers;

public record SellerTaxId(string Value)
{
    public virtual bool Equals(SellerTaxId? other)
    {
        if (other == null) return false;
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}