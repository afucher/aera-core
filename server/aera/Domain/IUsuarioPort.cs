using System.Threading.Tasks;

namespace aera_core.Domain
{
    public interface IUsuarioPort
    {
        public User CriaUsuario(User user);
        public Task<User> ObterPor(string nomeDeUsuario);
    }
}