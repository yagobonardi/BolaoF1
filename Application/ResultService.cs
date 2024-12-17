using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public class ResultService
{
    public const int POLE_POINTS = 4;
    public const int FIRST_POINTS = 5;
    public const int SECOND_POINTS = 3;
    public const int THIRD_POINTS = 2;
    public const int FASTEST_LAP_POINTS = 1;

    private BolaoDb _bolaoDb {get; set;}

    public ResultService(BolaoDb bolaoDb)
    {
        _bolaoDb = bolaoDb;
    }
    
    public bool Process(int grandPrixId)
    {
        var winners = GetWinnersIds(grandPrixId);
        
        if (winners is null) return false;

        SetPointsToUsers(winners.PoleWinnersIds, POLE_POINTS);
        SetPointsToUsers(winners.FirstWinnersIds, FIRST_POINTS);
        SetPointsToUsers(winners.SecondWinnersIds, SECOND_POINTS);
        SetPointsToUsers(winners.ThirdWinnersIds, THIRD_POINTS);
        SetPointsToUsers(winners.FatestWinnersIds, FASTEST_LAP_POINTS);

        return true;
    }

    public Winners ? GetWinnersIds(int grandPrixId)
    {
        var result = GetGrandPrixResult(grandPrixId);

        if (result is null) return null;

        return new Winners()
        {
            PoleWinnersIds = GetUsersIdCorrectPole(result.PoleDriverId),
            FirstWinnersIds = GetUsersIdCorrectFirst(result.PoleDriverId),
            SecondWinnersIds = GetUsersIdCorrectSecond(result.PoleDriverId),
            ThirdWinnersIds = GetUsersIdCorrectThird(result.PoleDriverId),
            FatestWinnersIds = GetUsersIdCorrectFatestLap(result.PoleDriverId)
        };
    }

    public async void SetPointsToUsers(List<int> userIds, int points)
    {
        if (userIds is not null)
        {
            foreach(int id in userIds)
            {
                var user = await _bolaoDb.Users.FindAsync(id);
                if (user is null) continue;
                var actualPoints = user.Points += points;
                user.Points = actualPoints;
                await _bolaoDb.SaveChangesAsync();
            }
        }
    }

    public Result ? GetGrandPrixResult(int grandPrixId)
    {
        return _bolaoDb.Results.FirstOrDefault(f => f.GrandPrixId == grandPrixId);
    }

    public List<int> GetUsersIdCorrectPole(int poleDriverIdResult)
    {   
        return _bolaoDb.Guesses
            .Where(w => w.PoleDriverId == poleDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectFirst(int firstDriverIdResult)
    {
        return _bolaoDb.Guesses
            .Where(w => w.FirstDriverId == firstDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectSecond(int secondDriverIdResult)
    {
        return _bolaoDb.Guesses
            .Where(w => w.SecondDriverId == secondDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectThird(int thirdDriverIdResult)
    {
        return _bolaoDb.Guesses
            .Where(w => w.ThirdDriverId == thirdDriverIdResult)
            .Select(s => s.UserId)
            .ToList();
    }

    public List<int> GetUsersIdCorrectFatestLap(int fatestLapDriverIdResult)
    {
        return _bolaoDb.Guesses
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