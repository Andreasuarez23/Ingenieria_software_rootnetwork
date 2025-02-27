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
            Id = 0,
            UserStatus = UserStatus.Active,
            IsAdmin = userPostRequestDTO.isAdmin
        };

        user.Encrypt(userPostRequestDTO.password);
        
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
    public async Task<IActionResult> Get(
        [FromQuery] string? query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool? isAdmin = null) // Nuevo parámetro opcional
    {
        IDAOUser daoUser = this.daoFactory.CreateDAOUser();

        var (users, totalRecords) = await daoUser.GetAll(query, page, pageSize);

        // Filtrar por isAdmin si se proporciona
        if (isAdmin.HasValue)
        {
            users = users.Where(u => u.IsAdmin == isAdmin.Value).ToList();
        }

        return Ok(users);
    }
    //por id usuario 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "ID inválido."
            });
        }

        var daoUser = this.daoFactory.CreateDAOUser();
        
        // Intentamos obtener el usuario por ID
        var user = await daoUser.GetById(id);
        
        if (user == null)
        {
            return NotFound(new ErrorResponseDTO
            {
                success = false,
                message = "Usuario no encontrado."
            });
        }

        // Devuelve el usuario si se encuentra
        return Ok(user);
    }


    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete(int id)
    {
    // Validar ID del usuario
        if (id <= 0)
        {
            return BadRequest(new ErrorResponseDTO
            {
            success = false,
            message = "El ID del usuario es inválido."
            });
        }

        // Obtener el usuario desde la base de datos
        var userDao = this.daoFactory.CreateDAOUser();
        User user = await userDao.GetById(id);

        // Validar si el usuario existe
        if (user == null)
        {
            return NotFound(new ErrorResponseDTO
            {
                success = false,
                message = "Usuario no encontrado."
            });
        }

        // Llamar al método Update para manejar el cambio de estado
        await userDao.Update(user);

    // Retornar respuesta exitosa
        return Ok(new SuccessResponseDTO
        {
            Success = true,
            Message = "Usuario bloqueado exitosamente."
        });
}
    //lore up por id para react 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserPutRequestDTO userPutRequestDTO)
    {
        if (id != userPutRequestDTO.Id)
        {
            return BadRequest(new { success = false, message = "El ID no coincide." });
        }

        // Verificar si el usuario existe en la base de datos
        var daoUser = this.daoFactory.CreateDAOUser();
        var existingUser = await daoUser.GetById(id);

        if (existingUser == null)
        {
            return NotFound(new { success = false, message = "El usuario no existe." });
        }

        // Actualizar los campos del usuario solo si se proporcionan valores nuevos
        if (!string.IsNullOrEmpty(userPutRequestDTO.Name))
            existingUser.Name = userPutRequestDTO.Name;

        if (!string.IsNullOrEmpty(userPutRequestDTO.LastName))
            existingUser.LastName = userPutRequestDTO.LastName;

        if (!string.IsNullOrEmpty(userPutRequestDTO.Mail))
            existingUser.Mail = userPutRequestDTO.Mail;

        if (userPutRequestDTO.Birthdate.HasValue)
            existingUser.Birthdate = userPutRequestDTO.Birthdate.Value;

        if (!string.IsNullOrEmpty(userPutRequestDTO.Password))
            existingUser.Password = userPutRequestDTO.Password;

        // Guardar los cambios en la base de datos
        await daoUser.Save(existingUser);

        return Ok(new { success = true, message = "Usuario actualizado correctamente." });
    }
// fin put react

    [HttpPut(Name = "UpdateUser")]
    public async Task<IActionResult> Put(UserPutRequestDTO userPutRequestDTO)
    {
        if (userPutRequestDTO == null || userPutRequestDTO.Id <= 0)
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "Datos ingresados incorrectos."
            });
        }

        var daoUser = this.daoFactory.CreateDAOUser();

        // Intenta obtener el usuario existente
        var existingUser = await daoUser.GetById(userPutRequestDTO.Id);
        if (existingUser == null)
        {
            return NotFound(new ErrorResponseDTO
            {
                success = false,
                message = "El usuario no existe."
            });
        }

        // Actualiza los campos solo si se proporcionan
        if (!string.IsNullOrEmpty(userPutRequestDTO.Name))
            existingUser.Name = userPutRequestDTO.Name;

        if (!string.IsNullOrEmpty(userPutRequestDTO.LastName))
            existingUser.LastName = userPutRequestDTO.LastName;

        if (!string.IsNullOrEmpty(userPutRequestDTO.Mail))
            existingUser.Mail = userPutRequestDTO.Mail;

        if (userPutRequestDTO.Birthdate.HasValue)
            existingUser.Birthdate = userPutRequestDTO.Birthdate.Value;

        if (!string.IsNullOrEmpty(userPutRequestDTO.Password))
            existingUser.Password = userPutRequestDTO.Password;

        // Guarda los cambios
        await daoUser.Save(existingUser);

        return Ok(new
        {
            success = true,
            message = "Usuario actualizado correctamente."
        });
    }
}

