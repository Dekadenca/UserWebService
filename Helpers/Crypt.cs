

using BCrypt.Net;

namespace UserManagerApp.Helpers
{
    public class Crypt
    {
        public static string HashData (string data)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword (data);
        }

        public static bool ValidateData(string hash ,string data)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(data, hash);
        }
    }
}
