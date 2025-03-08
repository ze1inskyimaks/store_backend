using IdentityService.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class AppDbContext : IdentityDbContext<Account>
{
    public AppDbContext(DbContextOptions<AppDbContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });

        // Налаштування для IdentityUserRole<string>
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });
        });

        // Налаштування для IdentityUserToken<string>
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        });
    }
}