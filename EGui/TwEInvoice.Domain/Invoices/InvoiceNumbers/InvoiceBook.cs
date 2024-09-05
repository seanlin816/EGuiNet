using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices.Events;

namespace TwEInvoice.Domain.Invoices.InvoiceNumbers;

public class InvoiceBook : Entity
{
    private InvoiceBook(
        Ulid id,
        AccountingPeriod accountingPeriod,
        InvoiceTrack track,
        int startNumber,
        int endNumber,
        int currentNumber,
        int nextNumber,
        InvoiceBookStatus status
    ) : base(id)
    {
        AccountingPeriod = accountingPeriod;
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
        RaiseDomainEvent(new InvoiceNumberPicked(newId));
        return newId;
    }

    private InvoiceBook()
    {
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
            Ulid.NewUlid(), 
            accountingPeriod, 
            track, 
            startNumber, 
            endNumber, 
            startNumber,
            startNumber + 1, 
            InvoiceBookStatus.New);
    }
}