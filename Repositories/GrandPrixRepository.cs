
using Microsoft.EntityFrameworkCore;

public class GrandPrixRepository : IGrandPrixRepository {
    private readonly BolaoDb _context;

    public GrandPrixRepository(BolaoDb context){
        _context = context;
    }

    public async Task<GrandPrix> CreateGrandPrix(GrandPrix grandprix)
    {
        await _context.GrandPrixes.AddAsync(grandprix);
        return grandprix;
    }

    public async Task<List<GrandPrix>> CreatePrixes(List<GrandPrix> grandprixes)
    {
        await _context.GrandPrixes.AddRangeAsync(grandprixes);
        await _context.SaveChangesAsync();
        return grandprixes;
    }

    public async Task<bool> DeleteGrandPrixById(int id)
    {
        var grandprix = await _context.GrandPrixes.FindAsync(id);
        if (grandprix is not null) {
            _context.GrandPrixes.Remove(grandprix);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<GrandPrix>> GetAllGrandPrix()
    {
        return await _context.GrandPrixes.ToListAsync();
    }

    public async Task<GrandPrix?> GetGrandPrixById(int id)
    {
        return await _context.GrandPrixes.FindAsync(id);
    }

    public async Task<GrandPrix?> UpdateGrandPrix(GrandPrix updatedgrandprix)
    {
        var grandPrix = await _context.GrandPrixes.FindAsync(updatedgrandprix.Id);

        if (grandPrix is not null){
            grandPrix.Name = updatedgrandprix.Name;
            grandPrix.Date = updatedgrandprix.Date;
            await _context.SaveChangesAsync();
        }

        return grandPrix;    
    }
}