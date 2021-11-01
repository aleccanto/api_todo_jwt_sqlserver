using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoJWT.Controllers;
using TodoJWT.Data;
using TodoJWT.Models;
using Xunit;

namespace Teste_TodoJWT_VS_19.Controller
{
    public class UsuarioControllerTest
    {
        private readonly Usuario _usuario;

        private readonly UsuarioController _usuarioController;

        private readonly DbContextOptions<Contexto> _options;

        public UsuarioControllerTest()
        {
            _options = new DbContextOptionsBuilder<Contexto>()
                .UseInMemoryDatabase(databaseName: "Usuario")
                .Options;

            _usuario = new Usuario { Nome = "Teste", Username = "Teste123", Password = "123" };

            _usuarioController = new UsuarioController();
        }

        [Fact]
        public async void Teste_Post()
        {
            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();
                int expected = 201;

                var result = await _usuarioController.PostUserAsync(contexto, _usuario);

                var resultado = result as ObjectResult;

                Assert.Equal(expected, resultado.StatusCode);
            }

        }

        [Fact]
        public async void Teste_Get()
        {
            using (var contexto = new Contexto(_options))
            {
                //Arrange
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();
                int expected = 200;

                //Act
                var result = await _usuarioController.GetAllAsync(contexto);
                var objResult = result as ObjectResult;

                //Assert
                Assert.Equal(expected, objResult.StatusCode);

            }

        }
    }
}