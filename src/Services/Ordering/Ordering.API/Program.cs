using Ordering.API;
using Ordering.Application;
using Ordering.Infraestructure.Data.Extensions;
using Ordering.Infraestruture;

var builder = WebApplication.CreateBuilder(args);

// add services to the container

builder.Services
    .AddApplicationServices()
    .AddInfraestructureServices(builder.Configuration)
    .AddApiServices();


var app = builder.Build();

// Configure the HTTP request pipeline
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
