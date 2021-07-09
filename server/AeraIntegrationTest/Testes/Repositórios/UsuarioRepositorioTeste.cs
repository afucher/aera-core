using System.Linq;
using System.Threading.Tasks;
using aera_core;
using aera_core.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class UsuarioRepositorioTeste : BaseTesteBanco
    {
        [Test]
        public void DeveFazerHashDaSenha()
        {
            var usuario = new Usuário
            {
                Email = "a@a.com.br",
                Username = "a",
                Password = "senha"
            };
            var repositorio = GetService<IUsuárioPort>();
            repositorio.Criar(usuario);

            var usuarioBanco = _contextoParaTestes.Usuarios.First();
            usuarioBanco.Password.Should().NotBe("senha");
            BCrypt.Net.BCrypt.Verify("senha", usuarioBanco.Password).Should().BeTrue();
        }

        [Test]
        public async Task DeveBuscarPorNomeDeUsuario()
        {
            var usuario = new Usuário
            {
                Username = "nome de usuario",
                Password = "senhaaa",
                Email = "a@a.com.br"
            };
            _contextoParaTestes.GravaUsuario(usuario);
            
            var repositorio = GetService<IUsuárioPort>();
            var usuarioDoBanco = await repositorio.ObterPor(usuario.Username);

            usuarioDoBanco.Should().NotBeNull().And.BeEquivalentTo(usuario);
        }
        
        [Test]
        public async Task NãoDeveRetornarQuandoUsuarioNãoExiste()
        {
            var usuario = new Usuário
            {
                Username = "nome de usuario",
                Password = "senhaaa",
                Email = "a@a.com.br"
            };
            _contextoParaTestes.GravaUsuario(usuario);
            var repositorio = GetService<IUsuárioPort>();
            var usuarioDoBanco = await repositorio.ObterPor("nao_existe");

            usuarioDoBanco.Should().BeNull();
        }
    }
}