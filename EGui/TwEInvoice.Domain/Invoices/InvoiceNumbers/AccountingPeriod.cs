namespace TwEInvoice.Domain.Invoices.InvoiceNumbers;

public record AccountingPeriod(int TwYear, int MonthFirst, int MonthSecond)
{
    public static AccountingPeriod Parse(string periodString)
    {
        if (periodString.Length == 5)
        {
            var yearStr = periodString.Substring(0, 3);
            var monthStr = periodString.Substring(3, 2);
            return new AccountingPeriod(Convert.ToInt32(yearStr), Convert.ToInt32(monthStr), 0);
        }

        if (periodString.Length == 7)
        {
            var yearStr = periodString.Substring(0, 3);
            var month1Str = periodString.Substring(3, 2);
            var month2Str = periodString.Substring(5, 2);
            return new AccountingPeriod(Convert.ToInt32(yearStr), Convert.ToInt32(month1Str), Convert.ToInt32(month2Str));
        }

        throw new InvalidOperationException("Invalid period");
    }
}