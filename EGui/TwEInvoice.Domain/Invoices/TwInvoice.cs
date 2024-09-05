using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;
using TwEInvoice.Domain.Invoices.VO;

namespace TwEInvoice.Domain.Invoices;

public class TwInvoice : Entity
{
    private TwInvoice()
    {
    }

    private TwInvoice(Ulid id, InvoiceNumber invoiceNumber, Buyer buyer, Seller seller) : base(id)
    {
        InvoiceNumber = invoiceNumber;
        Buyer = buyer;
        Seller = seller;
        InvoiceNumberString = new InvoiceNumberString(invoiceNumber.ToString());
    }
    
    public InvoiceNumber InvoiceNumber { get; private set; }
    
    public InvoiceNumberString InvoiceNumberString { get; private set; }
    public Buyer Buyer { get; private set; }
    public Seller Seller { get; private set; }
    
    public static TwInvoice Issue(InvoiceNumber invoiceNumber, Buyer buyer, Seller seller)
    {
        return new TwInvoice(Ulid.NewUlid(), invoiceNumber, buyer, seller);
    }

    public static void Cancel(InvoiceNumber number)
    {
        
    }

    public static void Void(InvoiceNumber number)
    {
        
    }
    
    
}

public record InvoiceNumberString(string value);