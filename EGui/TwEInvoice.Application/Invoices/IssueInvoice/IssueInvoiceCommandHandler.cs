using MediatR;
using TwEInvoice.Application.Abstractions.Messaging;
using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Application.Invoices.IssueInvoice;

internal sealed class IssueInvoiceCommandHandler : ICommandHandler<IssueInvoiceCommand, IssueInvoiceCommandResult>
{
    private readonly IInvoiceBookRepository _invoiceBookRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private IPublisher _publisher;

    public IssueInvoiceCommandHandler(
        IInvoiceBookRepository invoiceBookRepository,
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher
    )
    {
        _invoiceBookRepository = invoiceBookRepository;
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<IssueInvoiceCommandResult>> Handle(IssueInvoiceCommand request,
        CancellationToken cancellationToken)
    {
        var invoiceBook =
            await _invoiceBookRepository.GetCurrentOpenBookAsync(request.Period, request.Seller.TaxId, null,
                cancellationToken);
        if (invoiceBook == null)
        {
            return Result.Failure<IssueInvoiceCommandResult>(Error.None);
        }

        var invoiceNumber = invoiceBook.PickANumber();
        _invoiceBookRepository.Update(invoiceBook);
        
        var invoice = TwInvoice.Issue(invoiceNumber, request.Buyer, request.Seller);
        _invoiceRepository.Add(invoice);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new IssueInvoiceCommandResult();
    }
}