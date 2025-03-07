using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data.Models;

public class Account : IdentityUser
{
    public string FullName { get; set; } = String.Empty;
}