using Microsoft.EntityFrameworkCore;

public static class GrandPrixEndpoint
{
    public static void RegisterGrandPrixEndpoints(this IEndpointRouteBuilder routes)
    {
        var grandPrix = routes.MapGroup("api/v1/grandprix");
        
        grandPrix.MapGet("/", async (BolaoDb db) => await db.GrandPrixes.ToListAsync());

        grandPrix.MapPost("/", async (BolaoDb db, GrandPrix grandPrix) => 
        {
            await db.GrandPrixes.AddAsync(grandPrix);
            await db.SaveChangesAsync();
            return Results.Created($"/grandprix/{grandPrix.Id}", grandPrix);
        });

        grandPrix.MapPost("/list", async (BolaoDb db, GrandPrix[] grandPrix) => 
        {
            await db.GrandPrixes.AddRangeAsync(grandPrix);
            await db.SaveChangesAsync();
            return Results.Created();
        });

        grandPrix.MapGet("/{id}", async (BolaoDb db, int id) => await db.GrandPrixes.FindAsync(id));

        grandPrix.MapPut("/{id}", async (BolaoDb db, GrandPrix updateGrandPrix, int id) =>
        {
            var grandPrix = await db.GrandPrixes.FindAsync(id);
            if (grandPrix is null) return Results.NotFound();
            grandPrix.Name = updateGrandPrix.Name;
            grandPrix.Date = updateGrandPrix.Date;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        grandPrix.MapDelete("/{id}", async (BolaoDb db, int id) =>
        {
            var grandPrix = await db.GrandPrixes.FindAsync(id);
            if (grandPrix is null) return Results.NotFound();
            db.GrandPrixes.Remove(grandPrix);
            await db.SaveChangesAsync();
            return Results.Ok();
        }); 
    }
}