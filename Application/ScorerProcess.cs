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

        if (result is null) return Tuple.Create(false, "sem resultados para processar");
        
        var guesses = await _guessRepository.GetGuessByGrandPrixId(idGrandPrix);

        if (guesses is null) return Tuple.Create(false, "sem palpites para processar");

        var winners = GetWinnersIds(result, guesses);
        
        if (winners is null) return Tuple.Create(false, "sem vencedores para pontuar");

        await _userRepository.UpdateUsersPoints(winners.PoleWinnersIds, POLE_POINTS);
        await _userRepository.UpdateUsersPoints(winners.FirstWinnersIds, FIRST_POINTS);
        await _userRepository.UpdateUsersPoints(winners.SecondWinnersIds, SECOND_POINTS);
        await _userRepository.UpdateUsersPoints(winners.ThirdWinnersIds, THIRD_POINTS);
        await _userRepository.UpdateUsersPoints(winners.FatestWinnersIds, FASTEST_LAP_POINTS);

        return Tuple.Create(true, "Pontuação processada");
    }

    public Winners GetWinnersIds(Result result, List<Guess> guesses)
    {   
        var poleWinnersIds = GetUsersIdCorrectPole(result.PoleDriverId, guesses);
        var firstWinnersIds = GetUsersIdCorrectFirst(result.FirstDriverId, guesses);
        var secondWinnersIds = GetUsersIdCorrectSecond(result.SecondDriverId, guesses);
        var thirdWinnersIds = GetUsersIdCorrectThird(result.ThirdDriverId, guesses);
        var fatestWinnersIds = GetUsersIdCorrectFatestLap(result.FastestLapDriverId, guesses);

        return new Winners()
        {
            PoleWinnersIds = poleWinnersIds,
            FirstWinnersIds = firstWinnersIds,
            SecondWinnersIds = secondWinnersIds,
            ThirdWinnersIds = thirdWinnersIds,
            FatestWinnersIds = fatestWinnersIds
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