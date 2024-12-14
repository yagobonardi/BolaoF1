public class Guess
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int GrandPrixId { get; set; }

    public int PoleDriverId { get; set; }

    public int FirstDriverId { get; set; }
    public int SecondDriverId { get; set; }
    public int ThirdDriverId { get; set; }

    public int FastestLapDriverId { get; set; }

    public int Points { get; set; }

    public DateTime RegisterDate { get; set; }
}