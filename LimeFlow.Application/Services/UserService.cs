using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken ct = default)
    {
        var email = User.NormalizeEmail(request.Email);

        if (await _userRepository.EmailExistsAsync(email, ct))
            throw new ConflictException("Email already registered.");

        var user = new User(request.Name, email, _passwordHasher.Hash(request.Password));

        _userRepository.Add(user);
        await _unitOfWork.CommitAsync(ct);

        return user.ToResponse();
    }

    public async Task<UserResponse> GetCurrentAsync(CancellationToken ct = default)
    {
        var user = await GetCurrentUserAsync(ct);
        return user.ToResponse();
    }

    public Task<UserResponse> UpdateCurrentAsync(UpdateUserRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 3):
        // 1. Load the current user with GetCurrentUserAsync.
        // 2. Call user.UpdateProfile(request.Name).
        // 3. Commit with _unitOfWork and return user.ToResponse().
        throw new NotImplementedException();
    }

    public Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 3):
        // 1. Load the current user with GetCurrentUserAsync.
        // 2. If !_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash) throw UnauthorizedException.
        // 3. Call user.ChangePassword(_passwordHasher.Hash(request.NewPassword)) and commit.
        throw new NotImplementedException();
    }

    public async Task DeleteCurrentAsync(CancellationToken ct = default)
    {
        var user = await GetCurrentUserAsync(ct);

        _userRepository.Remove(user);
        await _unitOfWork.CommitAsync(ct);
    }

    private async Task<User> GetCurrentUserAsync(CancellationToken ct) =>
        await _userRepository.GetByIdAsync(_currentUser.UserId, ct)
        ?? throw new NotFoundException(nameof(User), _currentUser.UserId);
}
