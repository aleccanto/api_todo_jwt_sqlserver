using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoJWT.Data;
using TodoJWT.Dto;
using TodoJWT.Models;
using TodoJWT.Services;

namespace TodoJWT_src.Services
{
    public class LoginServices
    {
        public async Task<dynamic> Logar(Contexto contexto, UsuarioDto usuarioDto)
        {
            Usuario usuario;
            try
            {
                usuario = await
                    contexto.Usuarios
                            .AsNoTracking()
                            .FirstOrDefaultAsync(
                                x => x.Username == usuarioDto.Username
                                && x.Password == usuarioDto.Password);

                if (usuario == null) return null;

                var tokenString = new TokenServices();

                return new
                {
                    Usuario = usuario.Nome,
                    Token = tokenString.GeracaoToken(usuario)
                };
            }
            catch
            {
                return null;
            }
        }
    }
}