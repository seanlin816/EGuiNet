using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace Innorhino.Infrastructure.Data.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<TwInvoice>
{
    public void Configure(EntityTypeBuilder<TwInvoice> builder)
    {
        builder.ToTable("tw_invoice");
        builder.Property(p => p.Id).ValueGeneratedNever().HasConversion<UlidToBytesConverter>();
        builder.HasKey(x => x.Id);
        builder.ComplexProperty(x => x.InvoiceNumber);
        builder.ComplexProperty(x => x.Buyer);
        builder.ComplexProperty(x => x.Seller);
        builder.Property(x => x.InvoiceNumberString)
            .HasConversion(x => x.value, value => new InvoiceNumberString(value));
        builder.HasIndex(x => x.InvoiceNumberString);
        // builder.HasIndex(x => x.InvoiceNumber).IsUnique();
        // builder.HasIndex(x => x.InvoiceNumber.InvoiceNumberString).IsUnique();
    }
}