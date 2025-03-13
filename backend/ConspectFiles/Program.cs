using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ConspectFiles.Data;
using System.Text;
using ConspectFiles.Interface;
using ConspectFiles.Repository;
using ConspectFiles.Middleware;

DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {"..\\.env"}));
var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
var jwtSecret = configuration["JWT_SECRET"];
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT_SECRET is not configured or empty");
}

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IConspectRepository, ConspectRepository>();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Hello World!";
})
.WithName("NoteFlow");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();