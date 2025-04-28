using ConspectFiles.Data;
using System.Text;
using ConspectFiles.Interface;
using ConspectFiles.Repository;
using ConspectFiles.Model;
using ConspectFiles.Services;
using ConspectFiles.Middleware;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

// Завантаження змінних середовища з .env файлу
DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {".env"}));
var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
var builder = WebApplication.CreateBuilder(args);

// Додавання сервісів до контейнера
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Налаштування MongoDB
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<IMongoCollection<AppUser>>(sp =>
{
    var database = sp.GetRequiredService<MongoDbService>().Database;
    return database.GetCollection<AppUser>("users");
});

// Перевірка конфігурації JWT
var jwtSecret = configuration["JWT_SECRET"];
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT_SECRET is not configured or empty");
}

// Налаштування автентифікації JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT_ISSUER"],
            ValidAudience = configuration["JWT_AUDIENCE"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT_SECRET"]))
        };
    });

// Налаштування авторизації
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
   
    options.AddPolicy("UserManagement", policy =>
        policy.RequireRole("Admin", "SuperAdmin"));
});

// Додавання OpenAPI
builder.Services.AddOpenApi();

// Реєстрація репозиторіїв та сервісів
builder.Services.AddScoped<IConspectRepository, ConspectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Налаштування HTTP конвеєра
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

// Коментуємо, щоб зберегти варіант з першого файлу
// app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Hello World!";
})
.WithName("NoteFlow");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();