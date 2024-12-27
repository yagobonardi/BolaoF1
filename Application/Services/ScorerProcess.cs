using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class ScorerProcess : IScorerProcess 
{
    public const int POLE_POINTS = 4;
    public const int FIRST_POINTS = 5;
    public const int SECOND_POINTS = 3;
    public const int THIRD_POINTS = 2;
    public const int FASTEST_LAP_POINTS = 1;

    private readonly IResultRepository _resultRepository;
    private readonly IGuessRepository _guessRepository;

    private readonly IUserRepository _userRepository;

    public ScorerProcess(IResultRepository resultRepository, 
                   IGuessRepository guessRepository,
                   IUserRepository userRepository) {
        _resultRepository = resultRepository;
        _guessRepository = guessRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Tuple<bool, string>> ProcessGrandPrixPoints(int idGrandPrix)
    {
        var result = await _resultRepository.GetResultByGrandPrixId(idGrandPrix);
        if (result is null) 
            return Tuple.Create(false, "sem resultados para processar");
        
        var guesses = await _guessRepository.GetGuessByGrandPrixId(idGrandPrix);
        if (guesses is null) 
            return Tuple.Create(false, "sem palpites para processar");

        var poleWinnersIds = GetUsersIdCorrectPole(result.PoleDriverId, guesses);
        var firstWinnersIds = GetUsersIdCorrectFirst(result.FirstDriverId, guesses);
        var secondWinnersIds = GetUsersIdCorrectSecond(result.SecondDriverId, guesses);
        var thirdWinnersIds = GetUsersIdCorrectThird(result.ThirdDriverId, guesses);
        var fatestWinnersIds = GetUsersIdCorrectFatestLap(result.FastestLapDriverId, guesses);

        if (poleWinnersIds.Any()) 
            await _userRepository.UpdateUsersPoints(poleWinnersIds, POLE_POINTS);
        if (firstWinnersIds.Any()) 
            await _userRepository.UpdateUsersPoints(firstWinnersIds, FIRST_POINTS);
        if (secondWinnersIds.Any()) 
            await _userRepository.UpdateUsersPoints(secondWinnersIds, SECOND_POINTS);
        if (thirdWinnersIds.Any()) 
            await _userRepository.UpdateUsersPoints(thirdWinnersIds, THIRD_POINTS);
        if (fatestWinnersIds.Any()) 
            await _userRepository.UpdateUsersPoints(fatestWinnersIds, FASTEST_LAP_POINTS);

        return Tuple.Create(true, "Pontuação processada");
    } 

    public List<int> GetUsersIdCorrectPole(int poleDriverIdResult, List<Guess> guesses)
    {   
        return guesses
            .Where(w => w.PoleDriverId == poleDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectFirst(int firstDriverIdResult, List<Guess> guesses)
    {
        return guesses
            .Where(w => w.FirstDriverId == firstDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectSecond(int secondDriverIdResult, List<Guess> guesses)
    {
        return guesses
            .Where(w => w.SecondDriverId == secondDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectThird(int thirdDriverIdResult, List<Guess> guesses)
    {
        return guesses
            .Where(w => w.ThirdDriverId == thirdDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectFatestLap(int fatestLapDriverIdResult, List<Guess> guesses)
    {
        return guesses
            .Where(w => w.FastestLapDriverId == fatestLapDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }
}