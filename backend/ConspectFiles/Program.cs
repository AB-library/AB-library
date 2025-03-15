using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ConspectFiles.Data;
using System.Text;
using ConspectFiles.Interface;
using ConspectFiles.Repository;
using ConspectFiles.Model;

DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {"..\\.env"}));
var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddSingleton<IMongoCollection<AppUser>>(sp =>
{
    var database = sp.GetRequiredService<MongoDbService>().Database;
    return database.GetCollection<AppUser>("users");
});

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_SECRET"]))
        };
    });

builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IConspectRepository, ConspectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseDeveloperExceptionPage();

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