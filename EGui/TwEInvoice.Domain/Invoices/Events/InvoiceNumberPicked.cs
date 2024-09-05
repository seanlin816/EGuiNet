using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Domain.Invoices.Events;

public record InvoiceNumberPicked(InvoiceNumber NewNumber) : IDomainEvent;
