namespace DeliveryU.Shared.Exception;

public class ProductNotFoundException : IOException
{
    public ProductNotFoundException() : base()
    {

    }
    public ProductNotFoundException(string? message) : base(message)
    {

    }
    public ProductNotFoundException(string? message, IOException? innerException) : base(message, innerException)
    {

    }
}