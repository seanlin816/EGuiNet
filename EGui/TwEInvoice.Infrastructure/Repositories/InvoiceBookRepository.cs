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

    public async Task<InvoiceBook> GetCurrentOpenBookAsync(AccountingPeriod period, string sellerTaxId, Ulid? groupId,
        CancellationToken cancellationToken = default)
    {
        // var books = DbContext.InvoiceBooks.AsQueryable().Where(x => x.Status != InvoiceBookStatus.Closed);
        
        var books = DbContext.InvoiceBooks.AsQueryable().Where(
            x => 
                x.AccountingPeriod.TwYear == period.TwYear &&
                 x.AccountingPeriod.MonthFirst == period.MonthFirst &&
                 x.SellerTaxId.Equals(new SellerTaxId(sellerTaxId)) &&
                 x.Status != InvoiceBookStatus.Closed
                 );
        if (groupId.HasValue){
            books = books.Where(x=>x.AllocatedGroupId != null && x.AllocatedGroupId.Value == groupId.Value);
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
        DbContext.InvoiceBooks.Update(invoiceBook);
    }
}