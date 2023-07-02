using IEscola.Auth.Models;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace IEscola.Auth.Repositories
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private readonly Settings _settings;

        public CredentialsRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<Credentials> LoginAsync(string apiKey, string secret, string type)
        {
            var api = _settings.Apis.FirstOrDefault(a => a.ApiKey == apiKey && a.Secret == secret && a.CredentialType == type);

            if (api == null) return default;

            return new Credentials { ApiKey = apiKey, CredentialsType = type };
        }
    }

    public interface ICredentialsRepository
    {
        Task<Credentials> LoginAsync(string apiKey, string secret, string type);
    }
}
