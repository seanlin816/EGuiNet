using System.Windows.Input;
using TwEInvoice.Application.Abstractions.Messaging;
using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Application.Invoices.CreateInvoiceBook;

internal sealed class CreateInvoiceBookCommandHandler 
    : ICommandHandler<CreateInvoiceBookCommand, CreateInvoiceBookCommandResult>
{
    private readonly IInvoiceBookRepository _invoiceBookRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateInvoiceBookCommandHandler(IInvoiceBookRepository invoiceBookRepository, IUnitOfWork unitOfWork)
    {
        _invoiceBookRepository = invoiceBookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateInvoiceBookCommandResult>> Handle(CreateInvoiceBookCommand request, CancellationToken cancellationToken)
    {
        var book = InvoiceBook.Create(
            accountingPeriod: request.AccountPeriod, 
            track: request.Track, 
            taxId: request.SellerTaxId,
            startNumber: request.StartNumber, 
            endNumber: request.EndNumber);
        _invoiceBookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateInvoiceBookCommandResult(book.Id);
    }
}
