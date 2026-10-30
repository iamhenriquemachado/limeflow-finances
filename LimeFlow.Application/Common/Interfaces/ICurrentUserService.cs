namespace LimeFlow.Application.Common.Interfaces;

public interface ICurrentUserService
{
    /// <summary>Id of the authenticated user. Throws <c>UnauthorizedException</c> when there is none.</summary>
    Guid UserId { get; }
}
