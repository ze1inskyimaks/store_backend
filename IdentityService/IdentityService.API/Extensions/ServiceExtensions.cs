using System.Text;
using IdentityService.Business.Implemintation;
using IdentityService.Business.Interface;
using IdentityService.Business.RabbitMq;
using IdentityService.Data;
using IdentityService.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // 🔹 Реєстрація Scoped сервісів
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<JwtService>();
        services.AddSingleton<CompanyCreatedProducer>();


        // 🔹 Налаштування БД
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // 🔹 Налаштування Identity
        services.AddIdentity<Account, IdentityRole>(options =>
            {
                // Налаштування пароля
                options.Password.RequireDigit = true;             // Чи має містити цифри
                options.Password.RequireLowercase = true;         // Чи має містити маленькі літери
                options.Password.RequireUppercase = true;         // Чи має містити великі літери
                options.Password.RequireNonAlphanumeric = false;   // Чи має містити спецсимволи (!@#$%^&)
                options.Password.RequiredLength = 6;              // Мінімальна довжина пароля
                options.Password.RequiredUniqueChars = 0;         // Мінімальна кількість унікальних символів

                // Інші налаштування
                options.User.RequireUniqueEmail = true;           // Чи повинен email бути унікальним
                options.Lockout.MaxFailedAccessAttempts = 5;      // Спроби перед блокуванням
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Час блокування
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // 🔹 Налаштування автентифікації та JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["boby"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                }
            };

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
                ),
            };
        });
    }
}
