using UserManagerApp.Models;

namespace UserManagerApp.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser (ref User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        User? GetUser(string value, string property);
        bool ValidateUser(string password, int id);
    }
}
