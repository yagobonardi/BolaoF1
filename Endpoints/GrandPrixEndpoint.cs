using Microsoft.EntityFrameworkCore;

public static class GrandPrixEndpoint
{
    public static void RegisterGrandPrixEndpoints(this IEndpointRouteBuilder routes)
    {
        var grandPrix = routes.MapGroup("api/v1/grandprix");
        
        grandPrix.MapGet("/", async (IGrandPrixRepository repository) => 
            await repository.GetAllGrandPrix());

        grandPrix.MapPost("/", async (IGrandPrixRepository repository, GrandPrix grandPrix) => 
            await repository.CreateGrandPrix(grandPrix));

        grandPrix.MapPost("/list", async (IGrandPrixRepository repository, List<GrandPrix> grandPrix) => 
            await repository.CreatePrixes(grandPrix));

        grandPrix.MapGet("/{id}", async (IGrandPrixRepository repository, int id) => 
            await repository.GetGrandPrixById(id));

        grandPrix.MapPut("/{id}", async (IGrandPrixRepository repository, GrandPrix updatedgrandprix) =>
            await repository.UpdateGrandPrix(updatedgrandprix));

        grandPrix.MapDelete("/{id}", async (IGrandPrixRepository repository, int id) =>
            await repository.DeleteGrandPrixById(id)); 
    }
}