using HemenBiletProje.Repositories;
using HemenBiletProje.Entities;
using System.Threading.Tasks;

namespace HemenBiletProje.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        // UserService'i, UserRepository'i constructor üzerinden alıyoruz.
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Login işlemi
        public async Task<User> LoginUserAsync(string username, string password)
        {
            return await _userRepository.LoginAsync(username, password);
        }

        // Kullanıcı kaydetme işlemi
        public async Task<bool> RegisterUserAsync(User user)
        {
            return await _userRepository.RegisterAsync(user);
        }
    }
}
