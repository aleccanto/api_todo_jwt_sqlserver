using Microsoft.EntityFrameworkCore;
using TodoJWT.Data;
using TodoJWT.Dto;
using TodoJWT.Models;
using TodoJWT.Services;
using Xunit;

namespace Teste_TodoJWT_VS_19.Services
{
    public class TokenServicesTest
    {
        private readonly UsuarioDto _usuarioDto;
        private readonly Usuario _usuario;

        private readonly TokenServices _usuarioService;

        private readonly DbContextOptions<Contexto> _options;


        public TokenServicesTest()
        {
            _usuario = new Usuario { Nome = "Teste", Username = "Teste123", Password = "123" };
            _usuarioDto = new UsuarioDto { Username = "Teste123", Password = "123" };
            _usuarioService = new TokenServices();
            _options = new DbContextOptionsBuilder<Contexto>()
           .UseInMemoryDatabase(databaseName: "Usuarios")
           .Options;

        }

        [Fact]
        public void Teste_TokenServices_GetToken()
        {
            using (var context = new Contexto(_options))
            {
                context.Usuarios.Add(_usuario);
                context.SaveChanges();
            }

            using (var context = new Contexto(_options))
            {
                var token = _usuarioService.GeracaoToken(_usuario);
                Assert.NotEmpty(token);
            }
        }



    }
}