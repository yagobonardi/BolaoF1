
public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;

    public DriverService(IDriverRepository driverRepository) {
        _driverRepository = driverRepository;
    }

    public async Task<Tuple<bool, string>> CreateDriver(CreateDriverDTO driver)
    {
        var driverEntity = new Driver() {
            Name = driver.Name,
            Team = driver.Team,
            Active = driver.Active
        };

        await _driverRepository.CreateDriver(driverEntity);

        return Tuple.Create(true, "Criado com sucesso");
    }
}