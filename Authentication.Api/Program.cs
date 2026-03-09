using Authentication.Api;
using Authentication.Api.Domain.Interfaces;
using Authentication.Api.Domain.Models;
using Authentication.Api.Infrastructure.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddOpenApi();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();

app.Run();
