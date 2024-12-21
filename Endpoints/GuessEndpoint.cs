using Microsoft.EntityFrameworkCore;

public static class GuessEndpoint
{
    public static void RegisterGuessEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/guess");

        users.MapGet("/", async (IGuessRepository repository) => 
            await repository.GetAllGuesss());

        users.MapPost("/", async (IGuessRepository repository, Guess guess) => 
            await repository.CreateGuess(guess));

        users.MapGet("/{id}", async (IGuessRepository repository, int id) => 
            await repository.GetGuessById(id));

        users.MapPut("/{id}", async (IGuessRepository repository, Guess updatedguess, int id) =>
            await repository.UpdateGuess(updatedguess));

        users.MapDelete("/{id}", async (IGuessRepository repository, int id) =>
            await repository.DeleteGuess(id));
    }
}