using Yarp.Gateway.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();


app.UseRouting();

app.MapControllers();

app.MapReverseProxy();

app.Run();


