
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository {
    private readonly BolaoDb _context;

    public UserRepository(BolaoDb context){
        _context = context;
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null) {
            _context.Users.Remove(user);
            return true;
        }

        return false;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
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