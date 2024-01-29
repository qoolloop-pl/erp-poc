using ErpModule.Infrastructure;
using ErpModule.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Logging.AddConsole();

var erpDbConnectionString = builder.Configuration.GetConnectionString("ErpDbConnection") ?? throw new InvalidOperationException("Connection string 'ErpDbConnection' not found.");
builder.Services.AddDbContext<ErpDbContext>(options => options.UseSqlite(erpDbConnectionString));

builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
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
