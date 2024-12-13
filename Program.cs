using BolaoF1.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
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

app.MapGet("/drivers", () => BolaoF1DB.GetDrivers());
app.MapGet("/grandprixes", () => BolaoF1DB.GetGrandPrixes());
app.MapGet("/setResults{idGrandPrix}", (int idGrandPrix) => ResultService.SetResult(idGrandPrix));

app.Run();
