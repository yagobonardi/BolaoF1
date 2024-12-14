using BolaoF1.DB;

public static class Drivers
{
    public static void RegisterDriverEndpoints(this IEndpointRouteBuilder routes)
    {
        var drivers = routes.MapGroup("/api/v1/drivers");

        drivers.MapGet("/", () => BolaoF1DB.GetDrivers());
    }
}