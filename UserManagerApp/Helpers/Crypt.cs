namespace UserManagerApp.Helpers
{
    public class Crypt
    {
        // HashData uses string "data" to encrypt data to a hash and return it
        public static string HashData (string data)
        {
            return BCrypt.Net.BCrypt.HashPassword (data);
        }

        // ValidateData uses string "data" to verify it agains string "hash"
        public static bool ValidateData(string data, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(data, hash);
        }
    }
}
