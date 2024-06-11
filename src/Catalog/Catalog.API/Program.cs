var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(
    config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly)
    );


var app = builder.Build();


// maps the routes to the Carter modules
app.MapCarter();




app.Run();
