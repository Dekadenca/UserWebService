using UserManagerApp.Data;
using UserManagerApp.Interfaces;
using UserManagerApp.Models;

namespace UserManagerApp.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly DataContext _context;
        public ApiKeyRepository(DataContext context)
        {
            _context = context;
        }

        // GetApiKey fetches apiKey with string {client} from database 
        public ApiKey? GetApiKey(string client)
        {
            return _context.ApiKeys.Where(k => k.Client == client).FirstOrDefault();
        }

        // ValidateApiKey validates string {apiKey} with values in database 
        public bool ValidateApiKey(string apiKey)
        {
            var savedApiKey = _context.ApiKeys.Where(k => k.Value == apiKey).FirstOrDefault();
            return savedApiKey != null;
        }
    }
}
