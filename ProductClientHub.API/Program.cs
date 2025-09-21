using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Register;

var builder = WebApplication.CreateBuilder(args);

// registra o DbContext com SQLite
builder.Services.AddDbContext<ProductClientHubDbContext>(options =>
    options.UseSqlite("Data Source=projectdb.db"));

// registra o use case
builder.Services.AddScoped<RegisterClientUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
