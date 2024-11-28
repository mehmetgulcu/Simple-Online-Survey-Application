using Simple_Online_Survey_Application.Entity.Dtos;

namespace Simple_Online_Survey_Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
