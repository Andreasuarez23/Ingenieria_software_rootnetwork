using Microsoft.AspNetCore.Mvc;
using entities_library.publishing;
using dao_library.Interfaces.publishing;
using dao_library.Interfaces;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PublishingUserController : ControllerBase
    
    {
        private readonly ILogger<PublishingUserController> _logger;
        private readonly IDAOFactory daoFactory;

        public PublishingUserController(
        ILogger<PublishingUserController> logger, 
        IDAOFactory daoFactory)
        {
            _logger = logger;
            this.daoFactory = daoFactory;
        }
        
        //public async Task<IActionResult> PublishingUser([FromBody] PublishingUser publishingUser)
        //{
            //var =publishingUser new PublishingUser
            //{
            //    Description = PublishingUserRequestDTO.Description,
            //    MediaUrl = publishingUser.MediaUrl,
            //    UserName = publishingUser.UserName,
            //};
            //_logger.Publishing.Add (Publishing);

        //}



        
        
    }
}
