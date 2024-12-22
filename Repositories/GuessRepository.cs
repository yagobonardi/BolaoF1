
using Microsoft.EntityFrameworkCore;

public class GuessRepository : IGuessRepository {
    private readonly BolaoDb _context;

    public GuessRepository(BolaoDb context) {
        _context = context;
    }

    public async Task<Guess> CreateGuess(Guess guess)
    {
        await _context.AddAsync(guess);
        await _context.SaveChangesAsync();
        return guess;
    }

    public async Task<bool> DeleteGuess(int id)
    {
        var guess = await _context.Guesses.FindAsync(id);
        if (guess is not null) {
            _context.Guesses.Remove(guess);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<Guess>> GetGuessByGrandPrixId(int grandprixid)
    {
       return await _context.Guesses.Where(w=> w.GrandPrixId == grandprixid).ToListAsync(); 
    }

    public async Task<List<Guess>> GetAllGuesss()
    {
        return await _context.Guesses.ToListAsync();
    }

    public async Task<Guess?> GetGuessById(int id)
    {
        return await _context.Guesses.FindAsync(id);
    }

    public async Task<Guess?> UpdateGuess(Guess updatedguess)
    {
        var guess = await _context.Guesses.FindAsync(updatedguess.Id);
        
        if (guess is not null) {
            guess.UserId = updatedguess.UserId;
            guess.PoleDriverId = updatedguess.PoleDriverId;
            guess.FastestLapDriverId = updatedguess.FastestLapDriverId;
            guess.FirstDriverId = updatedguess.FirstDriverId;
            guess.SecondDriverId = updatedguess.SecondDriverId;
            guess.ThirdDriverId = updatedguess.ThirdDriverId;
            guess.Points = updatedguess.Points;
            guess.GrandPrixId = updatedguess.GrandPrixId;
            guess.RegisterDate = DateTime.Now;
            
            await _context.SaveChangesAsync();
        }

        return guess;
    }
}