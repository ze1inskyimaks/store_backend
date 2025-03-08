using IdentityService.Business.Models.DTO;
using IdentityService.Data.Models;

namespace IdentityService.Business.Mapping;

public static class AccountMapping
{
    public static Account ToModel(AccountDTO accountDto)
    {
        var model = new Account()
        {
            Email = accountDto.Email,
            UserName = accountDto.UserName
        };

        return model;
    }

    public static AccountDTO ToDto(Account account)
    {
        var dto = new AccountDTO()
        {
            Id = long.TryParse(account.Id, out var id) ? id : null,
            Email = account.Email!,
            UserName = account.UserName!
        };

        return dto;
    }
}