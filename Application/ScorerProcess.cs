using System.Data.Common;
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
    
    public async Task<bool> ProcessGrandPrixPoints(int idGrandPrix)
    {
        var winners = await GetWinnersIds(idGrandPrix);
        
        if (winners is null) return false;

        await _userRepository.UpdateUsersPoints(winners.PoleWinnersIds, POLE_POINTS);
        await _userRepository.UpdateUsersPoints(winners.FirstWinnersIds, FIRST_POINTS);
        await _userRepository.UpdateUsersPoints(winners.SecondWinnersIds, SECOND_POINTS);
        await _userRepository.UpdateUsersPoints(winners.ThirdWinnersIds, THIRD_POINTS);
        await _userRepository.UpdateUsersPoints(winners.FatestWinnersIds, FASTEST_LAP_POINTS);

        return true;
    }

    public async Task<Winners> GetWinnersIds(int idGrandPrix)
    {   
        var result = await _resultRepository.GetResultByGrandPrixId(idGrandPrix);
        var guesses = await _guessRepository.GetAllGuessByGrandPrixId(idGrandPrix);

        return new Winners()
        {
            PoleWinnersIds = GetUsersIdCorrectPole(result.PoleDriverId, guesses),
            FirstWinnersIds = GetUsersIdCorrectFirst(result.PoleDriverId, guesses),
            SecondWinnersIds = GetUsersIdCorrectSecond(result.PoleDriverId, guesses),
            ThirdWinnersIds = GetUsersIdCorrectThird(result.PoleDriverId, guesses),
            FatestWinnersIds = GetUsersIdCorrectFatestLap(result.PoleDriverId, guesses)
        };
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

public record Winners
{
    public required List<int> PoleWinnersIds {get;set;}
    public required List<int> FirstWinnersIds {get;set;}
    public required List<int> SecondWinnersIds {get;set;}
    public required List<int> ThirdWinnersIds {get;set;}
    public required List<int> FatestWinnersIds {get;set;}
}