using System.Reflection;
using ErpModule.Infrastructure;
using ErpModule.Infrastructure.Data;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.WebApi.Helpers.OpenAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

var erpDbConnectionString = builder.Configuration.GetConnectionString("ErpDbConnection") ?? throw new InvalidOperationException("Connection string 'ErpDbConnection' not found.");

builder.Services.AddDbContext<ErpDbContext>(options => options.UseSqlite(erpDbConnectionString));

builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<SmartEnumDocumentFilter>(new List<Assembly> { typeof(TruckStatus).Assembly });
});

var app = builder.Build();

await app.Services.MigrateContext(typeof(ErpDbContext));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();
