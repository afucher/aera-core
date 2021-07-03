using System.Threading.Tasks;

namespace aera_core.Domain
{
    public class AutenticacaoServico
    {
        private IUsuarioPort _usuarioPort;
        public AutenticacaoServico(IUsuarioPort usuarioPort)
        {
            _usuarioPort = usuarioPort;
        }
        public async Task<User> Login(string nomeDeUsuario, string senha)
        {
            var usuario = await _usuarioPort.ObterPor(nomeDeUsuario);
            
            if (usuario.ValidaSenha(senha))
            {
                return usuario;
            }
            return null;
        }
    }
}