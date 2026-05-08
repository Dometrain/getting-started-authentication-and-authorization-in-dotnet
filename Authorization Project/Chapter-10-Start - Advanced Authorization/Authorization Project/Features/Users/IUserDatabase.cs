using StartcodeAuthorization.Features.UserProfile;

namespace StartcodeAuthorization.Features.Users;

public interface IUserDatabase
{
    void AddUser(User user);
    List<User> GetAllUsers();
    User? GetUserProfileById(int id);
    void UpdateUser(int userId, UpdateUserModel updateUser);
}