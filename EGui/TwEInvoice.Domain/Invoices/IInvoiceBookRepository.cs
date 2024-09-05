using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Domain.Invoices;

public interface IInvoiceBookRepository
{
    Task<InvoiceBook?> GetCurrentOpenBookAsync(AccountingPeriod period, string sellerTaxId, Ulid? groupId, CancellationToken cancellationToken = default);
    Task<InvoiceBook?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InvoiceBook>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InvoiceBook>?> GetByPeriodAndBySellerTaxIdAsync(string period, string sellerTaxId, CancellationToken cancellationToken = default);
    
    void Add(InvoiceBook invoiceBook);
    void Update(InvoiceBook invoiceBook);
}