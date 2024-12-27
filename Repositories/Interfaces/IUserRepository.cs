public interface IUserRepository {
    Task<List<User>> GetAllUsers();
    Task<User> CreateUser(CreateUserDTO user);
    Task<User?> GetUserById(int id);

    Task<User?> UpdateUser(User updateduser);
    Task<bool> DeleteUser(int id);

    Task<bool> UpdateUsersPoints(List<int> userids, int points);

    Task<bool> UpdateUserPoints(int userid, int points);
}