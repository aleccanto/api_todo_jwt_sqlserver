using Microsoft.EntityFrameworkCore;
using TodoJWT.Data;
using TodoJWT.Models;
using TodoJWT_src.Services;
using Xunit;

namespace Teste_TodoJWT_VS_19.Controller
{
    public class UsuarioServicesTest
    {

        private readonly Usuario _usuario;

        private readonly UsuarioServices _usuarioServices;

        private readonly DbContextOptions<Contexto> _options;

        public UsuarioServicesTest()
        {
            _usuario = new Usuario { Nome = "Teste", Username = "Teste123", Password = "123" };
            _usuarioServices = new UsuarioServices();
            _options = new DbContextOptionsBuilder<Contexto>()
            .UseInMemoryDatabase(databaseName: "Usuarios")
            .Options;
        }

        [Fact]
        public void Create_Usuario()
        {

            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();


                var service = new UsuarioServices();
                var userDb = service.CreateUserAsync(contexto, _usuario);


                Assert.NotNull(userDb);
                Assert.Equal(_usuario.Nome, userDb.Result.Nome);
                Assert.Equal(_usuario.Username, userDb.Result.Username);
                Assert.Equal(_usuario.Password, userDb.Result.Password);
                Assert.Equal(1, userDb.Id);
            }
        }

        [Fact]
        public void Get_Usuario()
        {

            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                var service = new UsuarioServices();

                var userDb = service.CreateUserAsync(contexto, _usuario);

                var user = service.FindById(contexto, userDb.Result.Id);

                Assert.NotNull(user.Result);
                Assert.Equal(userDb.Result.Nome, user.Result.Nome);
                Assert.Equal(userDb.Result.Username, user.Result.Username);
                Assert.Equal(userDb.Result.Password, user.Result.Password);
                Assert.Equal(1, user.Result.Id);
            }
        }

        [Fact]
        public void Get_Usuario_NotFound()
        {

            using (var contexto = new Contexto(_options))
            {

                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                var service = new UsuarioServices();

                var user = service.FindById(contexto, 2);

                Assert.Null(user.Result);

            }
        }

        [Fact]
        public async void Delete_Usuario()
        {

            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                var service = new UsuarioServices();

                var userDb = await service.CreateUserAsync(contexto, _usuario);

                var user = service.DeleteUserAsync(contexto, 1);

                Assert.True(user.Result);
            }
        }

        [Fact]
        public void Delete_Usuario_NotFound()
        {

            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                var service = new UsuarioServices();

                var user = service.DeleteUserAsync(contexto, 2);

                Assert.False(user.Result);
            }
        }

        [Fact]
        public void Update_Usuario()
        {

            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                var service = new UsuarioServices();

                var userDb = service.CreateUserAsync(contexto, _usuario);

                var user = service.UpdateUserAsync(contexto, new Usuario { Nome = "Teste2", Username = "Teste2", Password = "123" }, userDb.Result.Id);

                Assert.NotNull(user.Result);
                Assert.Equal("Teste2", user.Result.Nome);
                Assert.Equal("Teste2", user.Result.Username);
                Assert.Equal("123", user.Result.Password);
                Assert.Equal(1, user.Result.Id);
            }
        }

        [Fact]
        public void Update_Usuario_NotFound()
        {

            using (var contexto = new Contexto(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                var service = new UsuarioServices();

                var user = service.UpdateUserAsync(contexto, new Usuario { Nome = "Teste2", Username = "Teste2", Password = "123" }, 2);

                Assert.Null(user.Result);
            }
        }
    }
}
