using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class ResultEndpoint
{
    public static void RegisterResultEndpoints(this IEndpointRouteBuilder routes)
    {
        var results = routes.MapGroup("/api/v1/results");

        results.MapGet("/", async (IResultRepository repository) => 
            await repository.GetAllResults());

        results.MapPost("/", async (IResultRepository repository, Result result) => 
            await repository.CreateResult(result));

        results.MapGet("/{id}", async (IResultRepository repository, int id) => 
            await repository.GetResultById(id));

        results.MapPut("/{id}", async (IResultRepository repository, Result updatedresult) =>
        {
            await repository.UpdateResult(updatedresult);
        });

        results.MapDelete("/{id}", async (IResultRepository repository, int id) =>
        {
            await repository.DeleteResult(id);
        });
    }
}