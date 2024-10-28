using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;


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



        //obtenet comentarios por publicacion 
        [HttpGet("Post/{postId}")]
        public IActionResult GetAll(int postId)
        {
            try
            {
                var commentDao = _daoFactory?.CreateDAOComment();
                var comments = commentDao?.GetAll(postId);
                return Ok(comments);
            }

            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error obteniendo comentarios para la publicación {PostId}", postId);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}   



