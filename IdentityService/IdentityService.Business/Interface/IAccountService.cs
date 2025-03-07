using IdentityService.Business.Models.DTO;
using IdentityService.Data.Models;

namespace IdentityService.Business.Interface;

public interface IAccountService
{
    public Task<string?> Login(AccountDTO accountDto);
    public Task Register(AccountDTO accountDto); 
}