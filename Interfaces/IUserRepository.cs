using UserManagerApp.Models;

namespace UserManagerApp.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        bool UpdateUser(User user, int Id);
        bool DeleteUser(int Id);
        User? GetUser(int Id);
        bool ValidateUser(string password, int Id);
        bool UserExists(string value, string property);
    }
}
