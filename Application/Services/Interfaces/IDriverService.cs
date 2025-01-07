public interface IDriverService {
    Task<Tuple<bool, string>> CreateDriver(CreateDriverDTO driver);
    
    Task<List<GetDriversDTO>> GetDrivers();
}