using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
using web_api.mock;
using entities_library.login;
using dao_library.Interfaces.login;
using dao_library.Interfaces;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IDAOFactory daoFactory;

        public LoginController(
            ILogger<LoginController> logger, 
            IDAOFactory daoFactory)
        {
            _logger = logger;
            this.daoFactory = daoFactory;
        }

        [HttpPost(Name = "Login")]
        
        public async Task<IActionResult> Post(LoginRequestDTO loginRequestDTO)
        {
            IDAOUser daoUser = daoFactory.CreateDAOUser();
            IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();

            // Obtener usuario por email y password
            var user = await daoUser.Get(loginRequestDTO.mail, loginRequestDTO.password);

            if (user == null || !user.IsPassword(loginRequestDTO.password))
            {
                return Unauthorized(new ErrorResponseDTO
                {
                    success = false,
                    message = "Incorrect email or password."
                });
            }

            // Verificar si el usuario está baneado
            var userBan = await daoUserBan.GetActiveBanByUserId(user.Id);
            if (userBan != null)
            {
                return Unauthorized(new ErrorResponseDTO
                {
                    success = false,
                    message = $"Your account has been banned. Reason: {userBan.Reason}. End of ban: {userBan.EndDateTime?.ToString("yyyy-MM-dd HH:mm") ?? "Permanent"}"
                });
            }

            // Si no está baneado, proceder con el login
            return Ok(new LoginResponseDTO
            {
                success = true,
                message = "", 
                id = user.Id,
                name = user.Name,
                lastName = user.LastName,
                description = user.Description,
                urlAvatar = "",
                mail = user.Mail,
                isAdmin = user.IsAdmin
            });
        }

    }
}

