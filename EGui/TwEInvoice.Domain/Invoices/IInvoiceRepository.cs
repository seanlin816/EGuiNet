namespace TwEInvoice.Domain.Invoices;

public interface IInvoiceRepository
{
    Task<TwInvoice?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TwInvoice>?> GetAllAsync(CancellationToken cancellationToken = default);
    
    void Add(TwInvoice invoice); 
}