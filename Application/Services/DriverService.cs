

using AutoMapper;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;

    private readonly IMapper _mapper;

    public DriverService(IDriverRepository driverRepository, IMapper mapper) {
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public async Task<List<GetDriversDTO>> GetDrivers()
    {
        var drivers = await _driverRepository.GetAllDrivers();
        
        var resp = _mapper.Map<List<GetDriversDTO>>(drivers);

        return resp;
    }
}