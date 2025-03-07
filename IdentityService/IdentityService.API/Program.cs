using System.Security.Claims;
using System.Text;
using IdentityService.Business.Implemintation;
using IdentityService.Business.Interface;
using IdentityService.Data;
using IdentityService.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Додаємо сервіси (Scoped)
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<JwtService>();   

// Отримуємо рядок підключення
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Додаємо контекст бази даних
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Вказуємо асемблер для міграцій

// Налаштування Identity
builder.Services.AddIdentity<Account, IdentityRole>()
    .AddRoles<IdentityRole>()  // 🔥 Додаємо підтримку ролей
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Налаштування автентифікації та JWT
builder.Services.AddAuthentication(options =>
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // 🔥 Має бути перед Authorization!
app.UseAuthorization();

app.MapControllers();
app.Run();
