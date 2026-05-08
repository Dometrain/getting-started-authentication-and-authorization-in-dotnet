using StartcodeAuthorization.Features.UserProfile;
using System.Security.Claims;

namespace StartcodeAuthorization.Features.Users;

public class UserDatabase : IUserDatabase
{
    private readonly static List<User> users;

    static UserDatabase() => users =
        [
            new()
            {
                UserID = 1,
                Website = "https://alice.dev",
                Country = "Canada",
                Claims =
                [
                    new("sub", "1"),
                    new("name", "Alice Svensson")
                ]
            },
            new()
            {
                UserID = 2,
                Website = "https://bobsite.com",
                Country = "Sweden",
                Claims =
                [
                        new("sub", "2"),
                        new("name", "Bob Andersson")
                ]
            },
            new()
            {
                UserID = 3,
                Website = "https://finance.com",
                Country = "Sweden",
                Claims =
                [
                        new("sub", "3"),
                        new("name", "Finance Andersson"),
                        new("email", "finance@example.com"),
                        new("country", "Sweden"),
                        new("JobTitle", "finance"),
                        new("role", "finance"),
                ]
            },
            new()
            {
                UserID = 4,
                Website = "https://sales.com",
                Country = "Denmark",
                Claims =
                [
                        new("sub", "4"),
                        new("name", "Sales Johnsson"),
                        new("email", "sales@example.com"),
                        new("JobTitle", "Sales"),
                        new("country", "Denmark"),
                        new("role", "sales")
                ]
            },
            new()
            {
                UserID = 5,
                Website = "https://ceo4you.com",
                Country = "Sweden",
                Claims =
                [
                        new("sub", "5"),
                        new("name", "Manager Wilson"),
                        new("email", "manager@example.com"),
                        new("JobTitle", "Manager"),
                        new("Country", "Sweden"),
                        new("role", "management"),
                        new("role", "admin"),
                ]
            },
            new User()
            {
                UserID = 6,
                Website = "https://admin.com",
                Country = "Denmark",
                Claims =
                [
                        new("sub", "6"),
                        new("name", "Admin Dilbertsson"),
                        new("email", "admin@example.com"),
                        new("JobTitle", "Admin"),
                        new("Country", "Sweden"),
                        new("role", "admin"),
                ]
            },
        ];

    public User? GetUserProfileById(int id)
    {
        return users.FirstOrDefault(user => user.UserID == id);
    }

    public List<User> GetAllUsers()
    {
        return new List<User>(users);
    }

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void UpdateUser(int userId, UpdateUserModel updatedUser)
    {
        var existingUser = GetUserProfileById(userId);

        if (existingUser != null && existingUser.Claims != null)
        {
            if (updatedUser.Name != null)
            {
                // Remove existing name claim
                existingUser.Claims.RemoveAll(c => c.Type == "name");

                // Add new name claim from the model
                existingUser.Claims.Add(new Claim("name", updatedUser.Name));
            }

            // Remove existing role claims
            existingUser.Claims.RemoveAll(c => c.Type == "role");

            // Add new role claims from the model
            if (updatedUser.Roles != null)
            {
                foreach (var role in updatedUser.Roles)
                {
                    existingUser.Claims.Add(new Claim("role", role));
                }
            }
        }
    }
}
