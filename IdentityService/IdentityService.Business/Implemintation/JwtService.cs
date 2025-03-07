using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Business.Implemintation;

public class JwtService
{
    private readonly UserManager<Account> _userManager;
    private readonly string _key;
    private readonly int _expiresInHours;
    
    public JwtService(IConfiguration configuration, UserManager<Account> userManager)
    {
        _userManager = userManager;
        _key = configuration["Jwt:Key"];
        _expiresInHours = int.Parse(configuration["Jwt:ExpiresInHours"]);
    }
    
    public async Task<string> GenerateJwtToken(Account account)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var secretKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(account);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Name, account.UserName!)
        };

        // Отримуємо всі клейми, які має користувач (якщо вони є)
        var userClaims = await _userManager.GetClaimsAsync(account);
        claims.AddRange(userClaims);

        // Додаємо ролі в токен
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role)); // 🔹 Правильний спосіб для Identity
        }
        
        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(_expiresInHours),
            signingCredentials: secretKey,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}