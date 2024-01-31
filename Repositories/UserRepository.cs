using UserManagerApp.Data;
using UserManagerApp.Helpers;
using UserManagerApp.Interfaces;
using UserManagerApp.Models;

namespace UserManagerApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) { 
            _context = context;
        }

        public bool AddUser(User user)
        {
            user.Password = Crypt.HashData(user.Password);
            //TODO: Only insert if username unique.
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user != null)
            {
                _context.Remove(user);
                return Save();
            }

            return false;
        }

        public User? GetUser(int Id)
        {
            return _context.Users.Where(u => u.Id == Id).FirstOrDefault();
        }

        public bool UpdateUser(User user, int Id)
        {
            //Check how this works and how it knows which user it is
            _context.Update(user);
            return Save();
        }

        public bool ValidateUser(string password, int Id)
        {
            var user = GetUser(Id);
            if (user != null)
            {
                return Crypt.ValidateData(password, user.Password);
            }

            return false;
        }

        public bool UserExists(string value, string property)
        {
            User? user = null;

            switch (property)
            {
                case "email":
                    user = _context.Users.Where(u => u.Email == property).FirstOrDefault();
                    break;
                case "username":
                    user = _context.Users.Where(u => u.UserName == property).FirstOrDefault();
                    break;

            }

            return user != null;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
