namespace PB201MovieApp.Business.Exceptions.CommonExceptions;

public class InvalidIdException : Exception
{
    public InvalidIdException()
    {
    }

    public InvalidIdException(string? message) : base(message)
    {
    }
}
