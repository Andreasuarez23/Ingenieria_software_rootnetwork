using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.comment;
using web_api.dto.common;
using web_api.dto.comment;
using entities_library.comment;
using web_api.dto;



[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IDAOFactory _daoFactory;
    private readonly ILogger<CommentController> _logger;

    public CommentController(ILogger<CommentController> logger, IDAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommentRequestDTO commentRequestDTO)
    {
        // Validación del texto del comentario
        if (string.IsNullOrEmpty(commentRequestDTO.Text))
        {
            return BadRequest(new
            {
                success = false,
                message = "El texto del comentario es obligatorio."
            });
        }

        try
        {
            // Validar que la publicación exista
            var publishing = await _daoFactory.CreateDAOPublishing().GetById(commentRequestDTO.PublishingId);
            if (publishing == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "La publicación sobre la que se intenta comentar no existe."
                });
            }

            // Validar que el usuario exista
            var user = await _daoFactory.CreateDAOUser().GetById(commentRequestDTO.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "El usuario que intenta comentar no existe."
                });
            }

            // Crear el comentario
            var comment = new Comment
            {
                Text = commentRequestDTO.Text,
                Date = DateTime.UtcNow,
                PublishingId = publishing.Id,
                User = user.Name
            };

            // Guardar el comentario
            var commentDAO = _daoFactory.CreateDAOComment();
            await commentDAO.Save(comment);

            // Crear la respuesta
            var response = new CommentResponseDTO
            {
                Id = comment.Id,
                Text = comment.Text,
                UserName = user.Name,
                PublishingId = publishing.Id
            };

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, new
            {
                success = true,
                message = "Comentario creado con éxito.",
                data = response
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el comentario.");
            return StatusCode(500, new
            {
                success = false,
                message = "Ocurrió un error inesperado al crear el comentario.",
                error = ex.Message
            });
        }
    }

    // Método GetComment ()
    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(long id)
    {
        try
        {
            var commentDAO = _daoFactory.CreateDAOComment();
            var comment = await commentDAO.GetById(id);

            if (comment == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "El comentario no existe."
                });
            }

            var response = new CommentResponseDTO
            {
                Id = comment.Id,
                Text = comment.Text,
                UserName = comment.User,
                //PublishingId = comment.PublishingId
            };

            return Ok(new
            {
                success = true,
                data = response
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el comentario.");
            return StatusCode(500, new
            {
                success = false,
                message = "Ocurrió un error inesperado al obtener el comentario.",
                error = ex.Message
            });
        }
    }


    [HttpGet("post/{postId}")]
    public async Task<IActionResult> GetCommentsByPostId(long postId)
    {
        try
        {
            var commentDAO = _daoFactory.CreateDAOComment();
            var comments = await commentDAO.GetCommentsByPostId(postId);

            if (comments == null || comments.Count == 0)
            {
                return NotFound(new { success = false, message = "No hay comentarios para esta publicación." });
            }

            var response = comments.Select(comment => new CommentResponseDTO
            {
                Id = comment.Id,
                Text = comment.Text,
                UserName = comment.User,
                PublishingId = comment.PublishingId ?? 0
            }).ToList();

            return Ok(new { success = true, data = response });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los comentarios de la publicación.");
            return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado.", error = ex.Message });
        }
    }


}

 