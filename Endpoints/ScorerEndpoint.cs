using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class ScorerEndpoint
{
    public static void RegisterScorerEndpoints(this IEndpointRouteBuilder routes)
    {
        var results = routes.MapGroup("/api/v1/scorer");

        results.MapPost("/process", async (IScorerProcessService service, int idgrandprix) => 
            await service.ProcessGrandPrixPoints(idgrandprix));
    }
}