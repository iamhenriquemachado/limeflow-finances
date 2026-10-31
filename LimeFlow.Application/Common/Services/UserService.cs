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

    public async Task<UserResponse> UpdateCurrentAsync(UpdateUserRequest request, CancellationToken ct = default)
    {
        var user = await GetCurrentUserAsync(ct);
        user.UpdateProfile(request.Name);
        await _unitOfWork.CommitAsync(ct);
        return user.ToResponse();
    }

    public async Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken ct = default)
    {
        var user = await GetCurrentUserAsync(ct);

        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash)) 
                throw new UnauthorizedException("Invalid password.");

        user.ChangePassword(_passwordHasher.Hash(request.NewPassword));
        await _unitOfWork.CommitAsync(ct);;
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
