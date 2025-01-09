

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;

    public DriverService(IDriverRepository driverRepository) {
        _driverRepository = driverRepository;
    }

    public async Task<List<GetDriversDTO>> GetDrivers()
    {
        var drivers = await _driverRepository.GetAllDrivers();
        
        var response = new List<GetDriversDTO>();

        foreach(var driver in drivers) {
            response.Add(new GetDriversDTO {
                Id = driver.Id,
                Name = driver.Name,
                Team = driver.Team
            });
        }

        return response;
    }
}