using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserBanController : ControllerBase
{
    private readonly ILogger<UserBanController> _logger;
    private readonly IDAOUserBan _daoUserBan;

    public UserBanController(ILogger<UserBanController> logger, IDAOUserBan daoUserBan)
    {
        _logger = logger;
        _daoUserBan = daoUserBan;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBan>>> GetBannedUsers()
    {
        try
        {
            var bannedUsers = await _daoUserBan.GetAll();
            return Ok(bannedUsers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching banned users.");
            return StatusCode(500, "Internal server error.");
        }
    }
}
