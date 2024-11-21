using MusicBoxAPI.Entity;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Interfaces.IService;
using MusicBoxAPI.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using DocumentFormat.OpenXml.Math;
using BCrypt.Net;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MusicBoxAPI.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string Register(RegisterRequest request)
        {


            // 2. 建立新用戶資料
            var userData = new UserData
            {
                UserId = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                // 建議使用 BCrypt 來雜湊密碼
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            // 3. 註冊用戶
            return _userRepository.RegisterUser(userData);
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        public LoginResponse Login(LoginRequest request)
        {
            var user = _userRepository.GetUserByUsername(request.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("使用者名稱或密碼錯誤");
            }

            // 驗證密碼 (應該使用密碼雜湊比較)
            if (!VerifyPassword(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("使用者名稱或密碼錯誤");
            }

            // 產生 JWT Token
            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Username = user.Name,
                Expiration = DateTime.UtcNow.AddHours(1)  // Token 有效期為1小時
            };
        }

        private string GenerateJwtToken(UserData user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            // 可以添加更多 claims
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
