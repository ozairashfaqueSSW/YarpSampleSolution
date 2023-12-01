using Yarp.Gateway.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp(builder.Configuration);

// Add services to the container.

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseStaticFiles();

app.UseRouting();

//app.UseCors();


app.MapReverseProxy();

app.Run();


