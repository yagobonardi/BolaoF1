public static class DriversEndpoint
{
    public static void RegisterDriverEndpoints(this IEndpointRouteBuilder routes)
    {
        var drivers = routes.MapGroup("/api/v1/drivers");

        drivers.MapGet("/", async (IDriverService service) => 
            await service.GetDrivers());
    }
}