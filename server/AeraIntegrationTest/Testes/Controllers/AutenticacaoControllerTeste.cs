using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using NUnit.Framework;
using BCrypt.Net;

namespace AeraIntegrationTest
{
    public class AutenticacaoControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task DeveRetornarAccessToken()
        {
            var usuario = UsuarioBuilder.Generate();
            _contextoParaTestes.GravaUsuario(usuario);
            var resposta = await _httpClient.PostAsJsonAsync("/api/autenticacao/login", new {usuario = usuario.Username, senha = "senha"} );

            resposta.Should().Be200Ok().And.Satisfy(new
            {
                access_token = default(string)
            }, model =>
            {
                model.access_token.Should().NotBeNull();
            });
        }
        
        [Test]
        public async Task DeveRetornarNãoAutorizadoQuandoUsuarioNãoExiste()
        {
            var resposta = await _httpClient.PostAsJsonAsync("/api/autenticacao/login",
                new {usuario = "usuario_que_nao_existe", senha = "senha_que_nao_existe"} );

            resposta.Should().Be401Unauthorized();
        }
    }
}