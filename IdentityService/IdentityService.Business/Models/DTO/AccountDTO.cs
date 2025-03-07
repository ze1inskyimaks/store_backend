namespace IdentityService.Business.Models.DTO;

public class AccountDTO
{
    public long? Id { get; set; }
    public string UserName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PasswordHash { get; set; }  = null!;
}