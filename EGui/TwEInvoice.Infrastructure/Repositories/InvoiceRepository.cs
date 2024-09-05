using TwEInvoice.Domain.Invoices;

namespace TwEInvoice.Infrastructure.Repositories;

internal sealed  class InvoiceRepository : Repository<TwInvoice>, IInvoiceRepository
{
    public InvoiceRepository(TwEInvoiceDbContext dbContext) : base(dbContext)
    {
    }
}