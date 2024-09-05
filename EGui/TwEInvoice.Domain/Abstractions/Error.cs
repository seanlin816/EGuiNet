namespace TwEInvoice.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new Error(string.Empty, String.Empty);
    
    public static Error NullValue = new Error("Error.NullValue", "Null value was provided");
}