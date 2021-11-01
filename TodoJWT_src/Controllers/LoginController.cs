using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoJWT.Data;
using TodoJWT.Dto;
using TodoJWT_src.Services;

namespace TodoJWT.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {

        //private readonly LoginServices _loginServices;
        private LoginServices _loginServices = new LoginServices();

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromServices] Contexto contexto,
            [FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid) return NotFound(new { message = "Não encontrado." });

            var usuario = await _loginServices.Logar(contexto, dto);

            if (usuario == null) return NotFound(new { message = "Não encontrado." });

            return usuario;
        }
    }
}

// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using TodoJWT.Data;
// using TodoJWT.Dto;
// using TodoJWT.Models;
// using TodoJWT.Services;

// namespace TodoJWT.Controllers
// {
//     [ApiController]
//     [Route("v1")]
//     public class LoginController : ControllerBase
//     {
//         [HttpPost("login")]
//         public async Task<ActionResult<dynamic>> Autenticar(
//             [FromServices] Contexto contexto,
//             [FromBody] UsuarioDto dto)
//         {
//             if (!ModelState.IsValid) return NotFound(new { message = "Não encontrado." });

//             Usuario usuario = await contexto.Usuarios
//                                     .AsNoTracking()
//                                     .FirstOrDefaultAsync(
//                                         x => x.Username == dto.Username
//                                         && x.Password == x.Password);

//             if (usuario == null) return NotFound(new { message = "Não encontrado." });

//             var tokenString = new TokenServices();

//             return new
//             {
//                 Usuario = usuario.Nome,
//                 token = tokenString.GeracaoToken(usuario)
//             };

//         }
//     }
// }
