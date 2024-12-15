using Microsoft.EntityFrameworkCore;

public static class GuessEndpoint
{
    public static void RegisterGuessEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/guess");

        users.MapGet("/", async (BolaoDb db) => await db.Guesses.ToListAsync());

        users.MapPost("/", async (BolaoDb db, Guess guess) => 
        {
            await db.Guesses.AddAsync(guess);
            await db.SaveChangesAsync();
            return Results.Created($"/guess/{guess.Id}", guess);
        });

        users.MapGet("/{id}", async (BolaoDb db, int id) => await db.Guesses.FindAsync(id));

        users.MapPut("/{id}", async (BolaoDb db, Guess updateguess, int id) =>
        {
            var guess = await db.Guesses.FindAsync(id);
            if (guess is null) return Results.NotFound();
            guess.UserId = updateguess.UserId;
            guess.PoleDriverId = updateguess.PoleDriverId;
            guess.FastestLapDriverId = updateguess.FastestLapDriverId;
            guess.FirstDriverId = updateguess.FirstDriverId;
            guess.SecondDriverId = updateguess.SecondDriverId;
            guess.ThirdDriverId = updateguess.ThirdDriverId;
            guess.Points = updateguess.Points;
            guess.GrandPrixId = updateguess.GrandPrixId;
            guess.RegisterDate = DateTime.Now;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        users.MapDelete("/{id}", async (BolaoDb db, int id) =>
        {
            var guess = await db.Guesses.FindAsync(id);
            if (guess is null) return Results.NotFound();
            db.Guesses.Remove(guess);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }
}