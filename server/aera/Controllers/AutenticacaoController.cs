using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace aera_core.Controllers
{
    
    
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login()
        {
            return Ok(new {access_token = geraJWT()});
        }
        
        private string geraJWT()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identityClaims = new ClaimsIdentity();
            var key = Encoding.ASCII.GetBytes("secret-bem-longa");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = "AERA", //appSettings.Value.Emissor,
                Audience = "a", //appSettings.Value.ValidoEm,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}