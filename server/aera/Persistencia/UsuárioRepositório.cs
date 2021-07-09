using System.Linq;
using System.Threading.Tasks;
using aera_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace aera_core.Persistencia
{
    public class UsuárioRepositório : IUsuárioPort
    {
        
        private readonly AplicaçãoContexto _contexto;
        public UsuárioRepositório(AplicaçãoContexto contexto)
        {
            _contexto = contexto;
        }


        public Usuário Criar(Usuário usuario)
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            var usuarioCriado = _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
            return usuarioCriado.Entity;
        }

        public Task<Usuário> ObterPor(string nomeDoUsuario)
        {
            return _contexto.Usuarios.Where(u => u.Username.Equals(nomeDoUsuario)).SingleOrDefaultAsync();
        }
    }
}