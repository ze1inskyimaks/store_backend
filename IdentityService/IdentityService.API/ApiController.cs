using System.Security.Claims;
using IdentityService.Business.Interface;
using IdentityService.Business.Models.DTO;
using IdentityService.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API;

[ApiController]
[Route("identity")]
public class ApiController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly UserManager<Account> _userManager;

    public ApiController(IAccountService accountService, UserManager<Account> userManager)
    {
        _accountService = accountService;
        _userManager = userManager;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]AccountDTO accountDto)
    {
        var token = await _accountService.Login(accountDto);

        if (token == null)
        {
            return Unauthorized();
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // Забороняє доступ через JavaScript
            Secure = true, // Включити у HTTPS
            Expires = DateTime.UtcNow.AddHours(12)
        };
        Response.Cookies.Append("boby", token, cookieOptions);
        
        return Ok(new { message = "Logged in successfully" });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]AccountDTO accountDto)
    {
        await _accountService.Register(accountDto);
        return Ok();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("add_role")]
    public async Task<IActionResult> SetRoleForUser(string id, string role)
    {
        await _accountService.AddRoleToAccount(id, role);
        return Ok();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("remove_role")]
    public async Task<IActionResult> RemoveRoleForUser(string id, string role)
    {
        await _accountService.RemoveRoleToAccount(id, role);
        return Ok();
    }
    
    [Authorize(Roles = "USER")]
    [HttpGet("userinfouser")]
    public IActionResult TakeSecretInfoUser()
    {
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        if (!roles.Contains("USER"))
        {
            return Unauthorized();
        }

        return Ok("Secret info for User");
    }
    
    [Authorize(Roles = "ADMIN")] //працю лише після оновлення jwt токена користувачем
    [HttpGet("userinfoadmin")]
    public IActionResult TakeSecretInfoAdmin()
    {
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        if (!roles.Contains("ADMIN"))
        {
            return Unauthorized();
        }

        return Ok("Secret info for Admin");
    }
}