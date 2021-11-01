using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoJWT.Models;

namespace TodoJWT.Services
{
    public class TokenServices
    {
        //private readonly IConfiguration configuration;
        private readonly string Secreto = ";0Pu2tlnJCdk1AeQsUzSlJiO1tpSRC5pZtZiUlMA5EF0dKyRk9FLKqjJZHHPWsBW";
        public string GeracaoToken(Usuario usuario)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(chave),
                        SecurityAlgorithms.HmacSha256Signature
                        )
            };

            var token = tokenManipulador.CreateToken(tokenDescriptor);

            return tokenManipulador.WriteToken(token);
        }
    }
}
