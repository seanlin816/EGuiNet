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
        builder.Property(p => p.AllocatedGroupId).ValueGeneratedNever().HasConversion<UlidToBytesConverter>();
        builder.HasKey(x => x.Id);
        builder.ComplexProperty(x => x.AccountingPeriod);
        builder.Property(x => x.SellerTaxId)
            .HasConversion(
                toDb => toDb.Value, 
                fromDb => new SellerTaxId(fromDb)
                );
        builder.Property(x => x.Track)
            .HasConversion(
                toDb => toDb.Value, 
                fromDb => new InvoiceTrack(fromDb)
            );
    }
}