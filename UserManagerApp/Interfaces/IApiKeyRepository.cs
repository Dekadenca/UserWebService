using UserManagerApp.Models;

namespace UserManagerApp.Interfaces
{
    public interface IApiKeyRepository
    {
        ApiKey? GetApiKey(string client);
        bool ValidateApiKey(string apiKey);
    }
}
