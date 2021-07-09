using System.Threading.Tasks;

namespace aera_core.Domain
{
    public interface IUsuárioPort
    {
        public Usuário Criar(Usuário usuário);
        public Task<Usuário> ObterPor(string nomeDeUsuario);
    }
}