using System;
using System.Collections.Generic;
using System.Linq;

namespace BolaoF1.DB; 

 public record Driver 
 {
   public int Id { get; set; } 
   public string Name { get; set; } = string.Empty;

   public string Team { get; set; } = string.Empty;
 }

public record GrandPrix 
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}

public record GrandPrixResult : Result
{
    public int Id { get; set; }
    public int GrandPrixId { get; set; }
}

public record Result
{
     public int PoleDriverId { get; set; }

    public int FirstDriverId { get; set; }
    public int SecondDriverId { get; set; }
    public int ThirdDriverId { get; set; }

    public int FastestLapDriverId { get; set; }
}

public record User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;

    public string CityState { get; set; } = string.Empty;

    public int Points { get; set; }
}

public record Guess : Result
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int GrandPrixId { get; set; }

    public DateTime RegisterDate { get; set; }
}

 public class BolaoF1DB
 {
    private static List<Driver> _drivers = new List<Driver>()
    {
        new Driver{Id = 1, Name = "Max Verstappen", Team = "RedBull" },
        new Driver{Id = 2, Name = "Sergio Perez", Team = "RedBull" },
        new Driver{Id = 3, Name = "Charles Leclerc", Team = "Ferrari" },
        new Driver{Id = 4, Name = "Lewis Hamilton", Team = "Ferrari" },
        new Driver{Id = 5, Name = "George Russell", Team = "Mercedes" },
        new Driver{Id = 6, Name = "Andrea Kimi Antonelli", Team = "Mercedes" },
        new Driver{Id = 7, Name = "Fernando Alonso", Team = "Aston Martin" },
        new Driver{Id = 8, Name = "Lance Stroll", Team = "Aston Martin" },
        new Driver{Id = 9, Name = "Lando Norris", Team = "McLaren" },
        new Driver{Id = 10, Name = "Oscar Piastri", Team = "McLaren" },
        new Driver{Id = 11, Name = "Pierre Gasly", Team = "Alpine" },
        new Driver{Id = 12, Name = "Jack Doohan", Team = "Alpine" },
        new Driver{Id = 13, Name = "Esteban Ocon", Team = "Haas" },
        new Driver{Id = 14, Name = "Oliver Bearman", Team = "Haas" },
        new Driver{Id = 15, Name = "Nico Hulkenberg", Team = "Sauber/Audi" },
        new Driver{Id = 16, Name = "Gabriel Bortoleto", Team = "Sauber/Audi" },
        new Driver{Id = 17, Name = "Alexander Albon", Team = "Williams" },
        new Driver{Id = 18, Name = "Carlos Sainz", Team = "Williams" },
        new Driver{Id = 19, Name = "Yuki Tsunoda", Team = "RB" },
        new Driver{Id = 20, Name = "Liam Lawson", Team = "RB" }
    };

    private static List<GrandPrix> _circuits = new List<GrandPrix>()
    {
        new GrandPrix {Id = 1, Name = "Australian Grand Prix", Date = new DateTime(2025, 3, 16)},
        new GrandPrix {Id = 2, Name = "Chinese Grand Prix", Date = new DateTime(2025, 3, 23)},
        new GrandPrix {Id = 3, Name = "Japanese Grand Prix", Date = new DateTime(2025, 4, 6)},
        new GrandPrix {Id = 4, Name = "Bahrain Grand Prix", Date = new DateTime(2025, 4, 13)},
        new GrandPrix {Id = 5, Name = "Saudi Arabian Grand Prix", Date = new DateTime(2025, 4, 20)},
        new GrandPrix {Id = 6, Name = "Emilia Romagna Grand Prix", Date = new DateTime(2025, 5, 18)},
        new GrandPrix {Id = 7, Name = "Monaco Grand Prix", Date = new DateTime(2025, 5, 25)},
        new GrandPrix {Id = 8, Name = "Spanish Grand Prix", Date = new DateTime(2025, 6, 1)},
        new GrandPrix {Id = 9, Name = "Canadian Grand Prix", Date = new DateTime(2025, 6, 15)},
        new GrandPrix {Id = 10, Name = "Austrian Grand Prix", Date = new DateTime(2025, 6, 29)},
        new GrandPrix {Id = 11, Name = "British Grand Prix", Date = new DateTime(2025, 7, 6)},
        new GrandPrix {Id = 12, Name = "Belgian Grand Prix", Date = new DateTime(2025, 7, 27)},
        new GrandPrix {Id = 13, Name = "Hungarian Grand Prix", Date = new DateTime(2025, 8, 3)},
        new GrandPrix {Id = 14, Name = "Dutch Grand Prix", Date = new DateTime(2025, 8, 31)},
        new GrandPrix {Id = 15, Name = "Italian Grand Prix", Date = new DateTime(2025, 9, 7)},
        new GrandPrix {Id = 16, Name = "Azerbaijan Grand Prix", Date = new DateTime(2025, 9, 21)},
        new GrandPrix {Id = 17, Name = "Singapore Grand Prix", Date = new DateTime(2025, 10, 5)},
        new GrandPrix {Id = 18, Name = "United States Grand Prix", Date = new DateTime(2025, 10, 19)},
        new GrandPrix {Id = 19, Name = "Mexican Grand Prix", Date = new DateTime(2025, 10, 26)},
        new GrandPrix {Id = 20, Name = "Brazilian Grand Prix", Date = new DateTime(2025, 11, 9)},
        new GrandPrix {Id = 21, Name = "Las Vegas Grand Prix", Date = new DateTime(2025, 11, 22)},
        new GrandPrix {Id = 22, Name = "Qatar Grand Prix", Date = new DateTime(2025, 11, 30)},
        new GrandPrix {Id = 23, Name = "Abu Dhabi Grand Prix", Date = new DateTime(2025, 12, 7)}
    };

    private static List<GrandPrixResult> _grandPrixResults = new List<GrandPrixResult>()
    {

    };

    private static List<Guess> _guesses = new List<Guess>()
    {
        new Guess
        {
            Id = 1,
            UserId = 101,
            GrandPrixId = 1,
            RegisterDate = new DateTime(2025, 3, 10),
            PoleDriverId = 1,
            FirstDriverId = 1,
            SecondDriverId = 3,
            ThirdDriverId = 2,
            FastestLapDriverId = 1
        },
        new Guess
        {
            Id = 2,
            UserId = 102,
            GrandPrixId = 2,
            RegisterDate = new DateTime(2025, 3, 15),
            PoleDriverId = 1,
            FirstDriverId = 1,
            SecondDriverId = 2,
            ThirdDriverId = 4,
            FastestLapDriverId = 2
        },
        new Guess
        {
            Id = 3,
            UserId = 103,
            GrandPrixId = 3,
            RegisterDate = new DateTime(2025, 4, 2),
            PoleDriverId = 3,
            FirstDriverId = 3,
            SecondDriverId = 1,
            ThirdDriverId = 2,
            FastestLapDriverId = 3
        },
        new Guess
        {
            Id = 4,
            UserId = 101,
            GrandPrixId = 4,
            RegisterDate = new DateTime(2025, 4, 8),
            PoleDriverId = 5,
            FirstDriverId = 5,
            SecondDriverId = 3,
            ThirdDriverId = 6,
            FastestLapDriverId = 6
        }
    };

    private static List<User> _users = new List<User>()
    {
        new User
        {
            Id = 1,
            Name = "Alice Silva",
            Mail = "alice.silva@example.com",
            CityState = "São Paulo, SP",
            Points = 120
        },
        new User
        {
            Id = 2,
            Name = "Bruno Pereira",
            Mail = "bruno.pereira@example.com",
            CityState = "Rio de Janeiro, RJ",
            Points = 95
        },
        new User
        {
            Id = 3,
            Name = "Carla Santos",
            Mail = "carla.santos@example.com",
            CityState = "Belo Horizonte, MG",
            Points = 110
        },
        new User
        {
            Id = 4,
            Name = "Diego Costa",
            Mail = "diego.costa@example.com",
            CityState = "Curitiba, PR",
            Points = 130
        },
        new User
        {
            Id = 5,
            Name = "Eduarda Martins",
            Mail = "eduarda.martins@example.com",
            CityState = "Porto Alegre, RS",
            Points = 105
        }
    };

   public static List<Driver> GetDrivers() 
    {
        return _drivers;
    } 

    public static List<GrandPrix>GetGrandPrixes()
    {
        return _circuits;
    }

    public static List<GrandPrixResult> GetGrandPrixResults()
    {
        return _grandPrixResults;
    }

    public static List<Guess> GetGuesses()
    {
        return _guesses;
    }

    public static bool SetGrandPrixResult(GrandPrixResult grandPrix)
    {
        _grandPrixResults.Add(grandPrix);
        
        return true;
    }

    public static bool UpdateUserPoints(List<int> usersId, int points) 
    {
        //UPDATE tbl_user SET points = points + 4 WHERE Id in (1,2,3,4,5)
        
        return true;
    } 
}