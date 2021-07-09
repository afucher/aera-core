using System.Threading.Tasks;
using aera_core;
using aera_core.Domain;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AeraUnitTest.Domain
{
    public class AutenticacaoServicoTeste
    {
        [Test]
        public async Task DeveRetornarUsuarioQuandoAutenticar()
        {
            var usuario = Substitute.For<Usuário>();
            usuario.ValidaSenha("senha").Returns(true);
            var mockUsuarioRepo = Substitute.For<IUsuárioPort>();
            mockUsuarioRepo.ObterPor("usuario").Returns(usuario);
            var servico = new Autenticador(mockUsuarioRepo);

            var usuarioLogado = await servico.Login("usuario","senha");

            usuarioLogado.Should().NotBeNull();
        }
        
        [Test]
        public async Task DeveRetornarNuloQuandoNãoAutenticar()
        {
            var usuario = Substitute.For<Usuário>();
            usuario.ValidaSenha("senha").Returns(false);
            var mockUsuarioRepo = Substitute.For<IUsuárioPort>();
            mockUsuarioRepo.ObterPor("usuario").Returns(usuario);
            var servico = new Autenticador(mockUsuarioRepo);

            var usuarioLogado = await servico.Login("usuario","senha");

            usuarioLogado.Should().BeNull();
        }
    }
}