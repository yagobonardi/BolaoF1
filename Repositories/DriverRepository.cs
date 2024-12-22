
using Microsoft.EntityFrameworkCore;

public class DriverRepository : IDriverRepository
{
    private readonly BolaoDb _context;

    public DriverRepository(BolaoDb context)
    {
        _context = context;
    }

    public async Task<Driver> CreateDriver(Driver driver)
    {
        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();
        return driver;
    }

    public async Task<List<Driver>> CreateDrivers(List<Driver> drivers)
    {
       await _context.Drivers.AddRangeAsync(drivers);
       await _context.SaveChangesAsync(); 
       return drivers;
    }

    public async Task<bool> DeleteDriverById(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver is not null) {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<Driver>> GetAllDrivers()
    {
        return await _context.Drivers.ToListAsync();
    }

    public async Task<Driver?> GetDriverById(int id)
    {
        return await _context.Drivers.FindAsync(id);
    }

    public async Task<Driver?> UpdateDriver(Driver updatedriver)
    {
        var driver = await _context.Drivers.FindAsync(updatedriver.Id);
        
        if (driver is not null) {
            driver.Name = updatedriver.Name;
            driver.Team = updatedriver.Team;
            driver.Active = updatedriver.Active;
            await _context.SaveChangesAsync();
        }

        return driver;
    }
}