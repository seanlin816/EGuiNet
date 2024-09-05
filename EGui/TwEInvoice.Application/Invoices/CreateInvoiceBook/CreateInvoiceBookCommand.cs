using System.Windows.Input;
using TwEInvoice.Application.Abstractions.Messaging;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Application.Invoices.CreateInvoiceBook;

public record CreateInvoiceBookCommand(
    AccountingPeriod AccountPeriod, 
    InvoiceTrack Track,
    SellerTaxId SellerTaxId, 
    int StartNumber, 
    int EndNumber) : ICommand<CreateInvoiceBookCommandResult>;