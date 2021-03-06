using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using aera_core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace aera_core.Controllers
{
    
    
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private Autenticador _autenticador;
        private readonly IOptions<TokenSettings> _tokenSettings;

        public AutenticacaoController(Autenticador autenticador, IOptions<TokenSettings> tokenSettings)
        {
            _autenticador = autenticador;
            _tokenSettings = tokenSettings;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var usuario = await _autenticador.Login(login.Usuario, login.Senha);
            if (usuario == null) return Unauthorized();
            return Ok(new {access_token = geraJWT()});
        }
        
        private string geraJWT()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identityClaims = new ClaimsIdentity();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _tokenSettings.Value.Emissor,
                Audience = _tokenSettings.Value.ValidoEm,
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.Value.MinutosParaExpirar),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}