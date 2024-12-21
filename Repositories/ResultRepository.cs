
using Microsoft.EntityFrameworkCore;

public class ResultRepository : IResultRepository
{
    private readonly BolaoDb _context;
    public ResultRepository(BolaoDb context)
    {
        _context = context;
    }
    public async Task<Result> CreateResult(Result result)
    {
       await _context.Results.AddAsync(result);
       await _context.SaveChangesAsync();
       return result; 
    }

    public async Task<bool> DeleteResult(int id)
    {
        var result = await _context.Results.FindAsync(id);
        if (result is not null) {
            _context.Results.Remove(result);
            return true;
        }

        return false;
    }

    public async Task<List<Result>> GetAllResults()
    {
        return await _context.Results.ToListAsync(); 
    }

    public async Task<Result?> GetResultByGrandPrixId(int grandprixid)
    {
        return await _context.Results.FirstOrDefaultAsync(f => f.GrandPrixId == grandprixid);
    }

    public async Task<Result?> GetResultById(int id)
    {
        return await _context.Results.FindAsync(id);
    }

    public async Task<Result?> UpdateResult(Result updatedresult)
    {
        var result = await _context.Results.FindAsync(updatedresult.Id);
        if (result is not null) {
            result.PoleDriverId = updatedresult.PoleDriverId;
            result.FastestLapDriverId = updatedresult.FastestLapDriverId;
            result.FirstDriverId = updatedresult.FirstDriverId;
            result.SecondDriverId = updatedresult.SecondDriverId;
            result.ThirdDriverId = updatedresult.ThirdDriverId;
            result.GrandPrixId = updatedresult.GrandPrixId;

            await _context.SaveChangesAsync();
        }

        return result;
    }
}