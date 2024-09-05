using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace Innorhino.Infrastructure.Data.Configurations;

public class InvoiceBookConfiguration : IEntityTypeConfiguration<InvoiceBook>
{
    public void Configure(EntityTypeBuilder<InvoiceBook> builder)
    {
        builder.ToTable("invoice_books");
        builder.Property(p => p.Id).ValueGeneratedNever().HasConversion<UlidToBytesConverter>();
        builder.HasKey(x => x.Id);
    }
}