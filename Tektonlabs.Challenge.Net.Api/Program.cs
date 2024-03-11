using Tektonlabs.Challenge.Net.Api.Extensions;
using Tektonlabs.Challenge.Net.Application;
using Tektonlabs.Challenge.Net.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigration();
//app.SeedData();
app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();