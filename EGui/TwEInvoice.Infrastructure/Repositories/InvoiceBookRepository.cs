using Microsoft.EntityFrameworkCore;
using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Infrastructure.Repositories;

internal sealed class InvoiceBookRepository : Repository<InvoiceBook>, IInvoiceBookRepository
{
    public InvoiceBookRepository(TwEInvoiceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<InvoiceBook> GetCurrentOpenBookAsync(string period, string sellerTaxId, Ulid? groupId,
        CancellationToken cancellationToken = default)
    {
        var accPeriod = AccountingPeriod.Parse(period);
        var books = DbContext.InvoiceBooks.Where(
            x => x.AccountingPeriod.TwYear == accPeriod.TwYear &&
                 x.AccountingPeriod.MonthFirst == accPeriod.MonthFirst &&
                 x.SellerTaxId.Value == sellerTaxId &&
                 x.Status != InvoiceBookStatus.Closed
                 ).OrderBy(x=>x.StartNumber);
        if (groupId.HasValue){
            books = books.Where(x=>x.AllocatedGroupId != null && x.AllocatedGroupId.Value == groupId.Value).OrderBy(x=>x.StartNumber);
        }
        var book = books.FirstOrDefault(x => x.Status == InvoiceBookStatus.Open);
        
        if (book is null)
        {
            book = books.FirstOrDefault(x => x.Status == InvoiceBookStatus.New);
        }

        return book;

    }

    public Task<IReadOnlyList<InvoiceBook>?> GetByPeriodAndBySellerTaxIdAsync(string period, string sellerTaxId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(InvoiceBook invoiceBook)
    {
        throw new NotImplementedException();
    }
}