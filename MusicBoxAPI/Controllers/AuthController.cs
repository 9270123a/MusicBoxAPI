using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Interfaces.IService;
using MusicBoxAPI.Models;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]

    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = _authService.Login(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    public ActionResult<string> Register([FromBody] RegisterRequest request)
    {
        try
        {
            // 基本驗證
            if (string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("所有欄位都必須填寫");
            }

            // 密碼強度驗證
            if (request.Password.Length < 6)
            {
                return BadRequest("密碼長度至少需要6個字元");
            }

            // 信箱格式驗證
            if (!IsValidEmail(request.Email))
            {
                return BadRequest("信箱格式不正確");
            }

            var result = _authService.Register(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "註冊過程發生錯誤");
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }



}