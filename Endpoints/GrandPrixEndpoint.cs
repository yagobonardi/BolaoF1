public static class GrandPrixEndpoint
{
    public static void RegisterGrandPrixEndpoints(this IEndpointRouteBuilder routes)
    {
        var grandPrix = routes.MapGroup("api/v1/grandprix");
        
        grandPrix.MapGet("/", async (IGrandPrixRepository repository) => 
            await repository.GetAllGrandPrix());

        grandPrix.MapGet("/{id}", async (IGrandPrixRepository repository, int id) => 
            await repository.GetGrandPrixById(id));
    }
}