using HelpFinance.Data;
using HelpFinance.Models;
using HelpFinance.Models.DTOs.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpFinance.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDataContext _ctx;

        public UsuarioRepository(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<UsuarioModel>> GetAllUsers() => await _ctx.Usuarios.ToListAsync();
        public async Task<UsuarioModel> GetUserID(int id) => await _ctx.Usuarios.FindAsync(id);


        public async Task<UsuarioModel> AddUser(RegisterUsuario usuario)
        {
            var hasher = new PasswordHasher<UsuarioModel>();
            var user = new UsuarioModel
            {
                NOME = usuario.NOME,
                LOGIN = usuario.LOGIN,
                SENHA = usuario.PASSWORD,
                EMAIL = usuario.EMAIL,
                TELEFONE = usuario.TELEFONE
            };
            user.SENHA = hasher.HashPassword(user, usuario.PASSWORD);
            await _ctx.Usuarios.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }


        public async Task UpdateUser(UsuarioModel usuario)
        {
            var existingUser = await _ctx.Usuarios.AsNoTracking()
                      .FirstOrDefaultAsync(u => u.ID == usuario.ID);

            if (existingUser == null)
                throw new Exception("Usuário não encontrado");
            if (!string.IsNullOrWhiteSpace(usuario.SENHA) &&
                usuario.SENHA != existingUser.SENHA)
            {
                var hasher = new PasswordHasher<UsuarioModel>();
                usuario.SENHA = hasher.HashPassword(usuario, usuario.SENHA);
            }
            else
            {
                usuario.SENHA = existingUser.SENHA;
            }

            _ctx.Entry(usuario).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _ctx.Usuarios.FindAsync(id);
            if (user != null)
            {
                _ctx.Usuarios.Remove(user);
                await _ctx.SaveChangesAsync();
            }
        }


    }
}
