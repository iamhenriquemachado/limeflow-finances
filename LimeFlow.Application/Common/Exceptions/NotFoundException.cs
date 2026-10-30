namespace LimeFlow.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string resourceName, Guid id)
        : base($"{resourceName} with id '{id}' was not found.") { }
}
