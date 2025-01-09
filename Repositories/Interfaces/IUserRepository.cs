public interface IUserRepository {
    Task<List<User>> GetAllUsers();
    Task<List<User>> GetUsersOrderByPoints();
    Task<int> CreateUser(CreateUserDTO user);
    Task<User?> GetUserById(int id);

    Task<User?> UpdateUser(User updateduser);
    Task<bool> DeleteUser(int id);

    Task<bool> UpdateUsersPoints(List<int> userids, int points);

    Task<bool> UpdateUserPoints(int userid, int points);

    bool VerifyLogin(LoginDTO login);
}