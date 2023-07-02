using IEscola.Auth.Models;
using System.Threading.Tasks;

namespace IEscola.Auth.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        string GenerateToken(Credentials credentials);
        ValidadeTokenResponse ValidateToken(ValidateToken validadeTokenRequest);
    }
}
