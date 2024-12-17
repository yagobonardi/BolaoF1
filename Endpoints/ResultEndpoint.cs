using Microsoft.EntityFrameworkCore;

public static class ResultEndpoint
{
    public static void RegisterResultEndpoints(this IEndpointRouteBuilder routes)
    {
        var results = routes.MapGroup("/api/v1/results");

        results.MapGet("/", async (BolaoDb db) => await db.Results.ToListAsync());

        results.MapPost("/", async (BolaoDb db, Result result) => 
        {
            await db.Results.AddAsync(result);
            await db.SaveChangesAsync();
            return Results.Created($"/results/{result.Id}", result);
        });

        results.MapGet("/{id}", async (BolaoDb db, int id) => await db.Results.FindAsync(id));

        results.MapPut("/{id}", async (BolaoDb db, Result updatresult, int id) =>
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

        results.MapDelete("/{id}", async (BolaoDb db, int id) =>
        {
            var result = await db.Results.FindAsync(id);
            if (result is null) return Results.NotFound();
            db.Results.Remove(result);
            await db.SaveChangesAsync();
            return Results.Ok();
        });

        results.MapPost("/processResult/{idgrandprix}", (ResultService resultservice, int idgrandprix) =>
        {
            var resultProcess = resultservice.Process(idgrandprix);
            return resultProcess ? Results.Ok() : Results.BadRequest();
        });
    }
}