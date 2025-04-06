using Serilog;
using EducationCenter.Api.Controllers;
using EducationCenter.Data.DbContexts;
using EducationCenter.Data.IRepositories;
using EducationCenter.Data.Repositories;
using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Mappers;
using EducationCenter.Service.Services;
using Microsoft.EntityFrameworkCore;
using EducationCenter.Api.Extensions;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option
    => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("EducationCenter.Data")));


builder.Services.AddCustomService();
builder.Services.AddAutoMapper(typeof(MappingProfile));


//Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//middleware
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/test",() => "yangi bilimni tekshirish uchun test");
app.Run();
