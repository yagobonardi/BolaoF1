using Microsoft.EntityFrameworkCore;

public static class UserEndpoint
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/users");

        users.MapGet("/", async (IUserRepository repository) => 
            await repository.GetAllUsers());

        users.MapPost("/", async (IUserRepository repository, CreateUserDTO user) => 
            await repository.CreateUser(user));

        users.MapGet("/{id}", async (IUserRepository repository, int id) => 
            await repository.GetUserById(id));

        users.MapPut("/{id}", async (IUserRepository repository, User updateduser) =>
            await repository.UpdateUser(updateduser));

        users.MapDelete("/{id}", async (IUserRepository repository, int id) =>
            await repository.DeleteUser(id));
    }
}