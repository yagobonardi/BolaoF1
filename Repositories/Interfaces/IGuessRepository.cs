public interface IGuessRepository {
    Task<List<Guess>> GetAllGuesss();
    Task<Guess> CreateGuess(Guess guess);
    Task<Guess?> GetGuessById(int id);

    Task<Guess?> UpdateGuess(Guess updatedguess);
    Task<bool> DeleteGuess(int id);
    Task<List<Guess>> GetAllGuessByGrandPrixId(int grandprixid);
}