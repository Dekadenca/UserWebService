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

        // AddUser adds user object with User {user} to database 
        public bool AddUser(ref User user)
        {
            user.Password = Crypt.HashData(user.Password);
            _context.Users.Add(user);
            return Save(); ;
        }

        // DeleteUser deletes user object with User {user} to database 
        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        // GetUser fetches user object with int {id} from database 
        public User? GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        // GetUser fetches user object with string {property} which is a value from CustomConstants
        //         and string {value} on which the user is checked against.
        public User? GetUser(string property, string value)
        {
            User? user = null;

            switch (property)
            {
                case CustomConstants.ID:                    
                    user = _context.Users.Where(u => u.Id == Int32.Parse(value)).FirstOrDefault();
                    break;
                case CustomConstants.EMAIL:
                    user = _context.Users.Where(u => u.Email == value).FirstOrDefault();
                    break;
                case CustomConstants.USERNAME:
                    user = _context.Users.Where(u => u.UserName == value).FirstOrDefault();
                    break;

            }

            return user;
        }

        // UpdateUser updates user object with User {user} in database
        public bool UpdateUser(User user)
        {
            user.Password = Crypt.HashData(user.Password);

            //Check how this works and how it knows which user it is
            _context.Update(user);
            return Save();
        }

        // ValidateUser validates string {password} against userID int {id}
        public bool ValidateUser(string password, int id)
        {
            var user = GetUser(id);
            if (user != null)
            {
                return Crypt.ValidateData(password, user.Password);
            }

            return false;
        }

        // Save saves changes to database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
