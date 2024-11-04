using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.comment;
using web_api.dto.common;
using web_api.dto.comment;
using entities_library.comment;



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


        // Obtener comentarios por publicación
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

        //Crear un comentario
        [HttpPost]
            public IActionResult CreateComment([FromBody] Comment comment)
        {
            try
            {
                var commentDao = _daoFactory?.CreateDAOComment();
                var createdComment = commentDao?.Create(comment);
                return CreatedAtAction(nameof(GetCommentById), new { commentId = createdComment?.Id }, createdComment);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creando el comentario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        //obtener comentarios por id
        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                var commentDao = _daoFactory?.CreateDAOComment();
                var comment = commentDao?.GetCommentsById(commentId);
                return comment != null ? Ok(comment) : NotFound();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error obteniendo el comentario {CommentId}", commentId);
                return StatusCode(500, "Error interno del servidor");
            }
        }
         // eliminar un comentario
        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            try
            {
                var commentDao = _daoFactory?.CreateDAOComment();
                var deleted = commentDao?.Delete(commentId);
                return deleted == true ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error eliminando el comentario {CommentId}", commentId);
                return StatusCode(500, "Error interno del servidor");
            }
        }

                // Actualizar un comentario existente
        [HttpPut("{commentId}")]
        public IActionResult UpdateComment(int commentId, [FromBody] Comment updatedComment)
        {
            try
            {
                var commentDao = _daoFactory?.CreateDAOComment();
                var comment = commentDao?.Update(commentId, updatedComment);
                return comment != null ? Ok(comment) : NotFound();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error actualizando el comentario {CommentId}", commentId);
                return StatusCode(500, "Error interno del servidor");
            }
        }








    } 
}



