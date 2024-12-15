using BolaoF1.DB;
using Microsoft.EntityFrameworkCore;

public static class UserEndpoint
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("/api/v1/users");

        users.MapGet("/", async (BolaoDb db) => await db.Users.ToListAsync());

        users.MapPost("/", async (BolaoDb db, User user) => 
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", user);
        });

        users.MapGet("/{id}", async (BolaoDb db, int id) => await db.Users.FindAsync(id));

        users.MapPut("/{id}", async (BolaoDb db, User updateuser, int id) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user is null) return Results.NotFound();
            user.Name = updateuser.Name;
            user.Mail = updateuser.Mail;
            user.CityState = updateuser.CityState;
            user.Points = updateuser.Points;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        users.MapDelete("/{id}", async (BolaoDb db, int id) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user is null) return Results.NotFound();
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }
}