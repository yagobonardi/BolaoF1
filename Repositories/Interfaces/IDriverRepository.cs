public interface IDriverRepository {
    Task<List<Driver>> GetAllDrivers();

    Task<Driver> CreateDriver(Driver driver);

    Task<List<Driver>> CreateDrivers(List<Driver> drivers);

    Task<Driver?> GetDriverById(int id);

    Task<Driver?> UpdateDriver(Driver updatedriver);

    Task<bool> DeleteDriverById(int id);
}