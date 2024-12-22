using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class ScorerEndpoint
{
    public static void RegisterScorerEndpoints(this IEndpointRouteBuilder routes)
    {
        var results = routes.MapGroup("/api/v1/scorer");

        results.MapPost("/process", async (IScorerProcess scorer, int idgrandprix) => 
            await scorer.ProcessGrandPrixPoints(idgrandprix));
    }
}