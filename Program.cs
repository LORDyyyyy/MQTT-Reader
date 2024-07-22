using App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<DataProcessingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    //test marioooooffff
    //hamda_helal

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
