using IdentityService.Extensions;
using IdentityService.Utilities;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Додаємо сервіси
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// 🔹 Налаштовуємо Middleware без виносу
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // 🔥 Має бути перед Authorization!
app.UseAuthorization();
app.MapControllers();

// 🔹 Створюємо ролі при старті програми
await RoleInitializer.EnsureRolesAsync(app.Services);

app.Run();

/*
{
    "id": 0,
    "userName": "string",
    "email": "string@email.com",
    "passwordHash": "strinG1"
}
*/