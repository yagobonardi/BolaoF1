using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BolaoDb>(options =>  options.UseNpgsql("Host=localhost:5432;Database=bolao;Username=admin;Password=admin"));

builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IGrandPrixRepository, GrandPrixRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGuessRepository, GuessRepository>();
builder.Services.AddScoped<IScorerProcessService, ScorerProcessService>();
builder.Services.AddScoped<IDriverService, DriverService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bolao F1 API", Version = "v1"});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bolao F1 API V1");
    });
}

app.RegisterDriverEndpoints();
app.RegisterUserEndpoints();
app.RegisterGrandPrixEndpoints();
app.RegisterGuessEndpoints();
app.RegisterResultEndpoints();
app.RegisterScorerEndpoints();

app.Run();
