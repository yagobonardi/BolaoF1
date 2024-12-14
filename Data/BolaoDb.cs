using Microsoft.EntityFrameworkCore;

public class BolaoDb : DbContext 
{
    public BolaoDb(DbContextOptions options) : base(options) { }
    public DbSet<Driver> Drivers {get; set;} = null!;
    public DbSet<GrandPrix> GrandPrixes {get; set;} = null!;

    public DbSet<Guess> Guesses {get; set;} = null!;
    public DbSet<Result> Results {get; set;} = null!;
    public DbSet<User> Users {get; set;} = null!;
}