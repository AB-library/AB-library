using BusinessLogic.Services;
using DataAccess.Models;
using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<DocumentRepository>();
builder.Services.AddSingleton<DocumentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var documentRepository = scope.ServiceProvider.GetRequiredService<DocumentRepository>();
    await documentRepository.EnsureIndexesAsync();
}

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();