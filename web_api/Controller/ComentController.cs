using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.comment;
using dao_library.entity_framework.comment;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController>? _logger;
        private readonly IDAOFactory? _daoFactory;

        public CommentController(
            ILogger<CommentController> logger,
            IDAOFactory daoFactory)
        {
            _logger = logger;
            _daoFactory = daoFactory;
        }
    }
}
