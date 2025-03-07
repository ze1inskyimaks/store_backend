using IdentityService.Business.Models.DTO;
using IdentityService.Data.Models;

namespace IdentityService.Business.Mapping;

public static class AccountMapping
{
    public static Account ToModel(AccountDTO accountDto)
    {
        var model = new Account()
        {
            FullName = accountDto.FullName,
            UserName = accountDto.UserName
        };

        return model;
    }

    public static AccountDTO ToDto(Account account)
    {
        var dto = new AccountDTO()
        {
            Id = long.TryParse(account.Id, out var id) ? id : null,
            FullName = account.FullName,
            UserName = account.UserName!
        };

        return dto;
    }
}