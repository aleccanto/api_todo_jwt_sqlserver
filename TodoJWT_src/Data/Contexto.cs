using Microsoft.EntityFrameworkCore;
using TodoJWT.Models;

namespace TodoJWT.Data
{
    public class Contexto : DbContext
    {
        //private string _stringConnectionMySql = "server=localhost;port=3306;database=dbTodo;uid=root";

        private string _stringConnectionSqlServer = @"Password=root;Persist Security Info=True;User ID=sa;Initial Catalog=dbTodoServer;Data Source=ACER\SQLEXPRESS";
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Todo> Todos { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     //     // optionsBuilder.UseMySql( 
        //     //     //     _stringConnectionMySql,
        //     //     //     ServerVersion.AutoDetect(_stringConnectionMySql)
        //     //     // );

        //     optionsBuilder.UseSqlServer(_stringConnectionSqlServer);

        // }

    }
}
