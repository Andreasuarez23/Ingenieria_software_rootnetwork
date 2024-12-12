using System.IO.Pipelines;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;


namespace web_api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IDAOFactory daoFactory;
    
    public UserController(
        ILogger<UserController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Post(UserPostRequestDTO userPostRequestDTO)
    {
        if(userPostRequestDTO == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "Datos ingresados Incorrectos. "
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.name))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El nombre es un dato obligatorio"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.lastName))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El apellido es un dato obligatorio"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.mail))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El correo electrónico es un dato obligatorio"
            });
        }

        if(userPostRequestDTO.birthdate == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "La fecha de nacimiento es un dato obligatorio"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.password))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El password es un dato obligatorio"
            });
        }

        User user = new User {
            LastName = userPostRequestDTO.lastName,
            Mail = userPostRequestDTO.mail,
            Name = userPostRequestDTO.name,
            Birthdate= userPostRequestDTO.birthdate,
            Description= "",
            File = null,
            Password = userPostRequestDTO.password,
            Id = 0,
            UserStatus = UserStatus.Active
        };
        await this.daoFactory.CreateDAOUser().Save(user);
        return Ok(new UserPostResponseDTO
        {
            id = user.Id,
            name = userPostRequestDTO.name,
            lastName = userPostRequestDTO.lastName,
            mail = userPostRequestDTO.mail
        });
    }

    [HttpGet(Name = "GetAll")]

    public async Task<IActionResult>Get(
        [FromQuery]UserGetAllRequestDTO request)
    {
        IDAOUser daoUser = this.daoFactory.CreateDAOUser();

        var (users, totalRecords) = await daoUser.GetAll(
            request.query,
            request.page,
            request.pageSize);

        return Ok(users); 

    
    }

    [HttpPut(Name = "UserUpdate")]
    

    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
    {
        return BadRequest(new ErrorResponseDTO
        {
            success = false,
            message = "El ID del usuario es inválido."
        });
    }
    
    // Obtener el usuario desde la base de datos.
    var userDao = this.daoFactory.CreateDAOUser();
    User user = await userDao.GetById(id);
    if (user == null)

        {
        return NotFound(new ErrorResponseDTO
        {
            success = false,
            message = "Usuario no encontrado."
        });
    }

    // Marcar al usuario como inactivo (baja lógica).
    user.UserStatus = UserStatus.Locked;

    // Guardar los cambios.
    await userDao.Update(user);


    return Ok(new SuccessResponseDTO
    {
        Success = true,
        Message = "Usuario bloqueado  exitosamente."
    });

    }
}

