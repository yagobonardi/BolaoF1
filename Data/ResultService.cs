using System.Collections.Generic;
using System.Linq;
using BolaoF1.DB;

public class ResultService
{
    public const int POLE_POINTS = 4;
    public const int FIRST_POINTS = 5;
    public const int SECOND_POINTS = 3;
    public const int THIRD_POINTS = 2;
    public const int FASTEST_LAP_POINTS = 1;

    public static bool DistributePoints(int grandPrixId)
    {
        var actualResult = BolaoF1DB.GetGrandPrixResults().FirstOrDefault(w=>w.GrandPrixId == grandPrixId);
        
        var guesses = BolaoF1DB.GetGuesses();

        if (actualResult is null || guesses is null)
            return false;

        var guessPoleList = guesses.Where(s => s.PoleDriverId == actualResult.PoleDriverId);
        var guessFirstList = guesses.Where(s => s.FirstDriverId == actualResult.FirstDriverId);
        var guessSecondList = guesses.Where(s => s.SecondDriverId == actualResult.SecondDriverId);
        var guessThirdList = guesses.Where(s => s.ThirdDriverId == actualResult.ThirdDriverId);
        var guessFatestList = guesses.Where(s => s.FastestLapDriverId == actualResult.FastestLapDriverId);

        SetPoints(guessPoleList.Select(s=>s.UserId).ToList(), POLE_POINTS);
        SetPoints(guessFirstList.Select(s=>s.UserId).ToList(), FIRST_POINTS);
        SetPoints(guessSecondList.Select(s=>s.UserId).ToList(), SECOND_POINTS);
        SetPoints(guessThirdList.Select(s=>s.UserId).ToList(), THIRD_POINTS);
        SetPoints(guessFatestList.Select(s=>s.UserId).ToList(), FASTEST_LAP_POINTS);

        return true;
    }

    public static void SetPoints(List<int> usersId, int points) 
    {
        BolaoF1DB.UpdateUserPoints(usersId, points);
    }
}