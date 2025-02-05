
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository {
    private readonly BolaoDb _context;

    public UserRepository(BolaoDb context){
        _context = context;
    }

    public async Task<int> CreateUser(CreateUserDTO user)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var userEntity = new User(){
            Name = user.Name,
            Mail = user.Mail,
            CityState = user.CityState,
            Points = 0,
            Password = passwordHash
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity.Id;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null) {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

        public async Task<List<User>> GetUsersOrderByPoints()
    {
        return await _context.Users.OrderBy(o => o.Points).ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> UpdateUser(User updateduser)
    {
        var user = await _context.Users.FindAsync(updateduser.Id);

        if (user is not null) {
            user.Name = updateduser.Name;
            user.Mail = updateduser.Mail;
            user.CityState = updateduser.CityState;
            user.Points = updateduser.Points;

            await _context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<bool> UpdateUserPoints(int userid, int points)
    {
        var user = await _context.Users.FindAsync(userid);

        if (user is null)
            return false;

        var userPoints = user.Points += points;

        user.Points = userPoints;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateUsersPoints(List<int> userids, int points)
    {
        foreach (var userid in userids)
        {
            var user = await _context.Users.FindAsync(userid);

            if (user is null)
                continue;

            var userPoints = user.Points += points;

            user.Points = userPoints;

            await _context.SaveChangesAsync();
        }

        return true;
    }
}