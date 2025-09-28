using HelpFinance.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpFinance.Data
{
    public class AppDataContext : DbContext
    {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }


        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<UsuarioDadosComplementares> DadosComplementaresUsuario { get; set; }
    }
}
