using TwEInvoice.Application.Abstractions.Messaging;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;
using TwEInvoice.Domain.Invoices.VO;

namespace TwEInvoice.Application.Invoices.IssueInvoice;

public record IssueInvoiceCommand(
    AccountingPeriod Period, 
    Buyer? Buyer, 
    Seller Seller
    ) : ICommand<IssueInvoiceCommandResult>;