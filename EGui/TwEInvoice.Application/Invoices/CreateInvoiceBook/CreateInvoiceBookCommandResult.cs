namespace TwEInvoice.Application.Invoices.CreateInvoiceBook;

public class CreateInvoiceBookCommandResult
{
    public CreateInvoiceBookCommandResult(Ulid id)
    {
        Id = id;
    }

    Ulid Id { get; set; }
}