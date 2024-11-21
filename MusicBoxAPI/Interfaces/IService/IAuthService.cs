using MusicBoxAPI.Models;

namespace MusicBoxAPI.Interfaces.IService
{
    public interface IAuthService
    {
        LoginResponse Login(LoginRequest request);
        string Register(RegisterRequest request);

    }
}
