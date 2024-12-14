using BolaoF1.DB;
using Microsoft.EntityFrameworkCore;

public static class ResultEndpoint
{
    public static void RegisterResultEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/results");

        users.MapGet("/", async (BolaoDb db) => await db.Results.ToListAsync());

        users.MapPost("/", async (BolaoDb db, Result result) => 
        {
            await db.Results.AddAsync(result);
            await db.SaveChangesAsync();
            return Results.Created($"/results/{result.Id}", result);
        });

        users.MapGet("/{id}", async (BolaoDb db, int id) => await db.Results.FindAsync(id));

        users.MapPut("/{id}", async (BolaoDb db, Result updatresult, int id) =>
        {
            var results = await db.Results.FindAsync(id);
            if (results is null) return Results.NotFound();
            results.PoleDriverId = updatresult.PoleDriverId;
            results.FastestLapDriverId = updatresult.FastestLapDriverId;
            results.FirstDriverId = updatresult.FirstDriverId;
            results.SecondDriverId = updatresult.SecondDriverId;
            results.ThirdDriverId = updatresult.ThirdDriverId;
            results.GrandPrixId = updatresult.GrandPrixId;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        users.MapDelete("/{id}", async (BolaoDb db, int id) =>
        {
            var result = await db.Results.FindAsync(id);
            if (result is null) return Results.NotFound();
            db.Results.Remove(result);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }
}