using System.Net;

namespace UserManagerApp.Helpers
{
    // Http helper functions
    public class Http
    {

        // GetComputerName asks dns for the name of owner string {clientIP}
        // Function makes a request to dns which makes its time consuption unreliable
        public static string GetComputerName(string clientIP)
        {
            try
            {
                // Asks dns for name and returns it
                var hostEntry = Dns.GetHostEntry(clientIP);
                return hostEntry.HostName;
            }
            catch (Exception e)
            {                
                // If something goes wrong, return empty string
                return string.Empty;
            }
        }
    }
}
