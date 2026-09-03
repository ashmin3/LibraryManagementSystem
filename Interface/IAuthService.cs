using LibraryManagementSystem.Dtos.Auth;

namespace LibraryManagementSystem.Interface
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Auth(LoginDto login);
    }
}
