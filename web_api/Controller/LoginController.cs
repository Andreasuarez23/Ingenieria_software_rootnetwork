using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
using web_api.mock;
using entities_library.login;

namespace web_api.Controllers;


[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Login")]
    public IActionResult Post(LoginRequestDTO loginRequestDTO)
    {
        foreach(User user in UserMock.Instance.Users)
        {
            if(loginRequestDTO != null &&
               loginRequestDTO.mail.ToLower().Equals(user.Mail) &&
               user.IsPassword(loginRequestDTO.password))
            {
                return Ok(new LoginResponseDTO 
                {
                    success = true,
                    message = "",
                    id = user.Id,
                    name = user.Name,
                    lastName = user.LastName,
                    description = user.Description,
                    urlAvatar = "",
                    mail = (string)user.Mail
                });
            }
        }
        
        
        return Unauthorized(new ErrorResponseDTO
        {
            success = false,
            message = "Invalid mail or password"
        });
    }
}
