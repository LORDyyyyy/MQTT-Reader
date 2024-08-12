using Microsoft.EntityFrameworkCore;
using AutoMapper;
using App;
using App.Data;
using App.Services;
using App.Interfaces;
using App.Repository;
using Quartz;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); // remove console logging

builder.Services.AddScoped<IReadingsProcessor, ReadingsProcessor>();

//builder.Services.AddHostedService<DataProcessingService>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IReadingLKPRepository, ReadingLKPRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<iActualReadingRepository, ActualReadingRepository>();


builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("DataProcessingService");

    q.AddJob<DataProcessingService>(options=>options.WithIdentity(jobKey));

    q.AddTrigger(options=> options.ForJob(jobKey).WithIdentity("DataProcessingServiceTrigger").WithCronSchedule("/5 * * ? * *"));

});

builder.Services.AddQuartzHostedService(q=>q.WaitForJobsToComplete = true);



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