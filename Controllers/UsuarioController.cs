using HelpFinance.Middleware;
using HelpFinance.Models;
using HelpFinance.Models.DTOs.Usuario;
using HelpFinance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpFinance.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly IAuthorizationMiddleware _authorizationMiddleware;
        public UsuarioController(IUsuarioRepository userRepository, IAuthorizationMiddleware authorizationMiddleware)
        {
            _userRepository = userRepository;
            _authorizationMiddleware = authorizationMiddleware;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> UsersList()
        {
            try
            {
                var usuarios = await _userRepository.GetAllUsers();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetUserByID/{id}")]
        public async Task<IActionResult> UserByID([FromRoute] int id)
        {
            try
            {
                var usuario = await _userRepository.GetUserID(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUsuario user)
        {
            try
            {
                var usuario = await _userRepository.AddUser(user);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromHeader] string Login, [FromHeader] string Password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                    return BadRequest("Login e senha são obrigatórios.");

                var token = await _authorizationMiddleware.LoginAsync(Login, Password);
                if (token is null) return Unauthorized();

                var cookieOptionsAccess = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // se for HTTPS → true
                    SameSite = SameSiteMode.Lax,
                    Expires = token.ExpiresAt,
                    Path = "/"
                };

                Response.Cookies.Append("jwt_token", token.AccessToken, cookieOptionsAccess);

                var cookieOptionsRefresh = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = token.RefreshExpiresAt,
                    Path = "/"
                };

                Response.Cookies.Append("refresh_token", token.RefreshToken, cookieOptionsRefresh);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UsuarioModel usuario)
        {
            try
            {
                await _userRepository.UpdateUser(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
