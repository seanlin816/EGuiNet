using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices.Events;

namespace TwEInvoice.Domain.Invoices.InvoiceNumbers;

public class InvoiceBook : Entity
{
    private InvoiceBook(
        Ulid id,
        AccountingPeriod accountingPeriod,
        InvoiceTrack track,
        SellerTaxId sellerTaxId,
        int startNumber,
        int endNumber,
        int currentNumber,
        int nextNumber,
        InvoiceBookStatus status
    ) : base(id)
    {
        AccountingPeriod = accountingPeriod;
        SellerTaxId = sellerTaxId;
        Track = track;
        StartNumber = startNumber;
        EndNumber = endNumber;
        CurrentNumber = currentNumber;
        NextNumber = nextNumber;
        Status = status;
    }

    public AccountingPeriod AccountingPeriod { get; private set; }

    public SellerTaxId SellerTaxId { get; private set; }
    public InvoiceTrack Track { get; private set; }
    public InvoiceBookStatus Status { get; private set; }

    public int StartNumber { get; private set; }
    public int EndNumber { get; private set; }
    public int CurrentNumber { get; private set; }
    public int NextNumber { get; private set; }

    public Ulid? AllocatedGroupId { get; private set; }

    public InvoiceNumber PickANumber()
    {
        var newId = new InvoiceNumber(Track.Value, CurrentNumber);
        CurrentNumber = NextNumber;
        NextNumber++;
        if (Status == InvoiceBookStatus.New)
        {
            Status = InvoiceBookStatus.Open;
            // TODO: Can Raise Invoice Status Changed event (Should this be in Event Handler?)
        }

        if (CurrentNumber > EndNumber)
        {
            Status = InvoiceBookStatus.Closed;
            // TODO: Can Raise Invoice Status Changed event (Should this be in Event Handler?)
        }
        RaiseDomainEvent(new InvoiceNumberPicked(newId));
        return newId;
    }

    private InvoiceBook()
    {
    }

    public void SetStatus(InvoiceBookStatus status)
    {
        if (Status != status)
        {
            // TODO: Invoice Status Changed Event
        }
        Status = status;
    }

    public void AllocateToGroup(Ulid groupId)
    {
        AllocatedGroupId = groupId;
    }

    public static InvoiceBook Create(
        AccountingPeriod accountingPeriod,
        InvoiceTrack track,
        SellerTaxId taxId,
        int startNumber,
        int endNumber
    )
    {
        return new InvoiceBook(
            id: Ulid.NewUlid(),
            accountingPeriod: accountingPeriod,
            track: track,
            startNumber: startNumber,
            endNumber: endNumber,
            currentNumber: startNumber,
            nextNumber: startNumber + 1,
            status: InvoiceBookStatus.New,
            sellerTaxId: taxId
        );
    }
}