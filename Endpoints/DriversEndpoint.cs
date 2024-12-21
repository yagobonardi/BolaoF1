public static class DriversEndpoint
{
    public static void RegisterDriverEndpoints(this IEndpointRouteBuilder routes)
    {
        var drivers = routes.MapGroup("/api/v1/drivers");

        drivers.MapGet("/", async (IDriverRepository repository) => 
            await repository.GetAllDrivers());

        drivers.MapPost("/", async (IDriverRepository repository, Driver driver) => 
            await repository.CreateDriver(driver));

        drivers.MapPost("/list", async (IDriverRepository repository, List<Driver> drivers) => 
            await repository.CreateDrivers(drivers));

        drivers.MapGet("/{id}", async (IDriverRepository repository, int id) => await repository.GetDriverById(id));

        drivers.MapPut("/{id}", async (IDriverRepository repository, Driver updatedriver) =>
            await repository.UpdateDriver(updatedriver));

        drivers.MapDelete("/{id}", async (IDriverRepository repository, int id) =>
            await repository.DeleteDriverById(id));
    }
}