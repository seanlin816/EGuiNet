using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Domain.Invoices;

public class DateTimeUtil
{
    public static AccountingPeriod GetCurrentAcctPeriod()
    {
        var acctYear = DateTime.Now.Year;
        var acctMonth = DateTime.Now.Month % 2 == 0 ? DateTime.Now.Month : DateTime.Now.Month + 1;
        return new AccountingPeriod(acctYear - 1911, acctMonth, 0);
    }
}