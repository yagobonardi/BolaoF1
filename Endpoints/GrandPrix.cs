using BolaoF1.DB;

public static class GrandPrix
{
    public static void RegisterGrandPrixEndpoints(this IEndpointRouteBuilder routes)
    {
        var grandPrix = routes.MapGroup("api/v1/grandprix");
        
        grandPrix.MapGet("/", () => BolaoF1DB.GetGrandPrixes());

        grandPrix.MapPost("/result", (GrandPrixResult grandPrix) => BolaoF1DB.SetGrandPrixResult(grandPrix));

        grandPrix.MapGet("/results", () => BolaoF1DB.GetGrandPrixResults());
        
        grandPrix.MapPost("/distributePoints", (int id) => ResultService.DistributePoints(id));
    }
}