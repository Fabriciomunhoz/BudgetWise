using HelpFinance.Models;
using HelpFinance.Models.DTOs.Usuario;

namespace HelpFinance.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<List<UsuarioModel>> GetAllUsers();
        public Task<UsuarioModel> GetUserID(int id);
        public Task<UsuarioModel> AddUser(RegisterUsuario usuario);
        public Task UpdateUser(UsuarioModel usuario);
        public Task DeleteUser(int id);
    }
}
