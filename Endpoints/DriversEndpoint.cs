using Microsoft.EntityFrameworkCore;

public static class DriversEndpoint
{
    public static void RegisterDriverEndpoints(this IEndpointRouteBuilder routes)
    {
        var drivers = routes.MapGroup("/api/v1/drivers");

        drivers.MapGet("/", async (BolaoDb db) => await db.Drivers.ToListAsync());

        drivers.MapPost("/", async (BolaoDb db, Driver driver) => 
        {
            await db.Drivers.AddAsync(driver);
            await db.SaveChangesAsync();
            return Results.Created($"/drivers/{driver.Id}", driver);
        });

        drivers.MapGet("/{id}", async (BolaoDb db, int id) => await db.Drivers.FindAsync(id));

        drivers.MapPut("/{id}", async (BolaoDb db, Driver updatedriver, int id) =>
        {
            var driver = await db.Drivers.FindAsync(id);
            if (driver is null) return Results.NotFound();
            driver.Name = updatedriver.Name;
            driver.Team = updatedriver.Team;
            driver.Active = updatedriver.Active;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        drivers.MapDelete("/{id}", async (BolaoDb db, int id) =>
        {
            var pizza = await db.Drivers.FindAsync(id);
            if (pizza is null) return Results.NotFound();
            db.Drivers.Remove(pizza);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }
}