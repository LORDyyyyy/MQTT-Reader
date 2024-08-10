using Microsoft.EntityFrameworkCore;
using AutoMapper;
using App;
using App.Data;
using App.Services;
using App.Interfaces;
using App.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReadingsProcessor, ReadingsProcessor>();
builder.Services.AddHostedService<DataProcessingService>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IReadingLKPRepository, ReadingLKPRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<iActualReadingRepository, ActualReadingRepository>();


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

var app = builder.Build();

if (args.Length >= 1 && args[0].ToLower() == "seeddata")
    SeedData(app);
void SeedData(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        Seeder.Seed(dbContext);
    }
}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();