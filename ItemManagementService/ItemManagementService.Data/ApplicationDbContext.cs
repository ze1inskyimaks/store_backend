using ItemManagementService.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(i => i.Price)
                .HasPrecision(18, 2) // Два знаки після коми
                .IsRequired();

            entity.Property(i => i.Description)
                .HasMaxLength(1000);

            entity.Property(i => i.StockQuantity)
                .IsRequired();

            entity.Property(i => i.Status)
                .HasConversion<int>() // Зберігаємо як int в БД
                .IsRequired();

            entity.Property(i => i.CompanyId)
                .IsRequired();

            entity.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()") // Автоматична дата створення
                .IsRequired();

            entity.Property(i => i.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}