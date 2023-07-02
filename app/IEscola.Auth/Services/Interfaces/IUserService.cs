using IEscola.Auth.Models;
using System.Threading.Tasks;

namespace IEscola.Auth.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> LoginAsync(string username, string password);
    }
}