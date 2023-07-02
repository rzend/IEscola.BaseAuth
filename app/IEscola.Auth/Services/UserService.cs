using IEscola.Auth.Models;
using IEscola.Auth.Repositories;
using IEscola.Auth.Services.Interfaces;
using IEscola.Core.Notificacoes.Interfaces;
using IEscola.Core.Services;
using System.Threading.Tasks;

namespace IEscola.Auth.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, INotificador notificador) : base(notificador)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await _userRepository.LoginAsync(username, password);
        }
    }
}
