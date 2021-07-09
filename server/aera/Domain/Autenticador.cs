using System.Threading.Tasks;

namespace aera_core.Domain
{
    public class Autenticador
    {
        private IUsuárioPort _usuárioPort;
        public Autenticador(IUsuárioPort usuárioPort)
        {
            _usuárioPort = usuárioPort;
        }
        public async Task<Usuário> Login(string nomeDeUsuario, string senha)
        {
            var usuario = await _usuárioPort.ObterPor(nomeDeUsuario);
            
            if (usuario != null && usuario.ValidaSenha(senha))
            {
                return usuario;
            }
            return null;
        }
    }
}