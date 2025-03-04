using System.Diagnostics;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.AspNetCore.Mvc;
using web_api.dto.login;

namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]

public class UserBanController : ControllerBase
{
    private readonly ILogger<UserBanController> _logger;
    private readonly IDAOFactory daoFactory;

    public UserBanController(
        ILogger<UserBanController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory; // Inyectamos la Factory
    }

    // Banear un usuario
    [HttpPost("ban/{userId}")]
    public async Task<IActionResult> BanUser(long userId, [FromBody] UserBanRequestDTO banRequest)
    {
        try
        {
            // Crear el DAO de usuario
            var userDao = daoFactory.CreateDAOUser();

            // Obtener el usuario desde la base de datos
            var user = await userDao.GetById(userId);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No se encontró un usuario con el ID {userId}"
                });
            }

            // Crear el objeto UserBan
            var userBan = new UserBan
            {
                User = user,
                StartDateTime = banRequest.StartDateTime ?? DateTime.UtcNow,
                EndDateTime = banRequest.EndDateTime,
                Reason = banRequest.Reason
            };

            // Crear el DAO para UserBan y guardar el baneo
            var userBanDao = daoFactory.CreateDAOUserBan();
            await userBanDao.Save(userBan);

            return Ok(new
            {
                success = true,
                message = "El usuario ha sido baneado exitosamente."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al intentar banear al usuario.");
            return StatusCode(500, new
            {
                success = false,
                message = "Error interno del servidor."
            });
        }
    }

    // Obtener todos los usuarios baneados
    [HttpGet]
    public async Task<IActionResult> GetBannedUsers(
        string? query = null,
        int page = 1,
        int pageSize = 10)
    {
        try
        {
            var userBanDao = daoFactory.CreateDAOUserBan();
            var (bannedUsers, total) = await userBanDao.GetAll(query, page, pageSize);

            var response = bannedUsers.Select(userBan => new UserBanResponseDTO
            {
                Id = userBan.Id,
                StartDateTime = userBan.StartDateTime,
                EndDateTime = userBan.EndDateTime,
                Reason = userBan.Reason,
                UserId = userBan.User.Id
            });

            return Ok(new
            {
                Total = total,
                Data = response
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los usuarios baneados.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    // Obtener un usuario baneado por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBannedUserById(long id)
    {
        try
        {
            var userBanDao = daoFactory.CreateDAOUserBan();
            var userBan = await userBanDao.GetById(id);

            if (userBan == null)
            {
                return NotFound(new { message = $"No se encontró un baneo con el ID {id}" });
            }

            var response = new UserBanResponseDTO
            {
                Id = userBan.Id,
                StartDateTime = userBan.StartDateTime,
                EndDateTime = userBan.EndDateTime,
                Reason = userBan.Reason,
                UserId = userBan.User.Id
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al obtener el baneo con ID {id}.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }

   
    // Desbloquear un usuario baneado
    [HttpPut("unlock/{id}")]
    public async Task<IActionResult> Unlock(long id)
    {
        try
        {
            var userBanDao = daoFactory.CreateDAOUserBan();
            var userBan = await userBanDao.GetById(id);

            if (userBan == null)
            {
                return NotFound(new { success = false, message = $"No se encontró un baneo con el ID {id}" });
            }

            await userBanDao.Unlock(id);

            return Ok(new { success = true, message = "El baneo fue desbloqueado exitosamente." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al desbloquear el baneo con ID {id}");
            return StatusCode(500, new { success = false, message = "Error interno del servidor." });
        }
    }
}

   
