using IdentityService.Business.Interface;
using IdentityService.Business.Mapping;
using IdentityService.Business.Models.DTO;
using IdentityService.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Business.Implemintation;

public class AccountService : IAccountService
{
    private readonly UserManager<Account> _userManager;
    private readonly JwtService _jwtService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountService(UserManager<Account> userManager, JwtService jwtService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _roleManager = roleManager;
    }
    
    public async Task<string?> Login(AccountDTO accountDto)
    {
        // Знайти користувача за його ім'ям
        var acc = await _userManager.FindByNameAsync(accountDto.UserName);
        if (acc == null)
        {
            throw new Exception("User indefinite!");
        }

        // Перевірка пароля для знайденого користувача
        var passwordValid = await _userManager.CheckPasswordAsync(acc, accountDto.PasswordHash);
        if (!passwordValid)
        {
            throw new Exception("Wrong password");
        }

        return await _jwtService.GenerateJwtToken(acc);
    }

    public async Task Register(AccountDTO accountDto)
    {
        if (await _userManager.FindByNameAsync(accountDto.UserName) != null)
        {
            throw new Exception("Користувач із таким ім’ям вже існує.");
        }
        
        var account = AccountMapping.ToModel(accountDto);

        var result = await _userManager.CreateAsync(account, accountDto.PasswordHash);
        if (!result.Succeeded)
        {
            throw new Exception("Не вдалося створити користувача: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        /*var roleExist = await _roleManager.RoleExistsAsync("USER");
        if (!roleExist)
        {
            // Якщо роль не існує, створюємо її
            var roleResult = await _roleManager.CreateAsync(new IdentityRole("USER"));
            if (!roleResult.Succeeded)
            {
                throw new Exception("Херня вийшла");
            }
        }*/
        
        await _userManager.AddToRoleAsync(account, "USER");
    }
}