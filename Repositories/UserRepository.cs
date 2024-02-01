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

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User? GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User? GetUser(string value, string property)
        {
            User? user = null;

            switch (property)
            {
                case CustomConstants.ID:                    
                    user = _context.Users.Where(u => u.Id == Int32.Parse(property)).FirstOrDefault();
                    break;
                case CustomConstants.EMAIL:
                    user = _context.Users.Where(u => u.Email == property).FirstOrDefault();
                    break;
                case CustomConstants.USERNAME:
                    user = _context.Users.Where(u => u.UserName == property).FirstOrDefault();
                    break;

            }

            return user;
        }
        public bool UpdateUser(User user)
        {
            user.Password = Crypt.HashData(user.Password);

            //Check how this works and how it knows which user it is
            _context.Update(user);
            return Save();
        }

        public bool ValidateUser(string password, int id)
        {
            var user = GetUser(id);
            if (user != null)
            {
                return Crypt.ValidateData(password, user.Password);
            }

            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
