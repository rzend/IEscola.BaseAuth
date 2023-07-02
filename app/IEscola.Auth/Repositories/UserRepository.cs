using IEscola.Auth.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEscola.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _Users = new List<User> {
            new User {Id = 1, Username = "bea", Password = "bea123", Role = "gerente" },
            new User {Id = 2, Username = "aline", Password = "aline123", Role = "coordenador" },
            new User {Id = 3, Username = "augusto", Password = "aug123", Role = "coordenador" },
            new User {Id = 3, Username = "antonio", Password = "ant123", Role = "peao" }
        };

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = _Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return await Task.FromResult(user);
        }
    }

    public interface IUserRepository
    {
       Task<User> LoginAsync(string username, string password);
    }
}
