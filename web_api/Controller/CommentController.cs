using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.comment;
using web_api.dto.common;
using web_api.dto.comment;
using entities_library.comment;
using web_api.dto;



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



        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequestDTO commentRequestDTO)
        {
            // Validar campos obligatorios
            if (string.IsNullOrEmpty(commentRequestDTO.Text))
                return BadRequest("El texto del comentario es obligatorio.");

             // Obtener usuario
            var userDAO = _daoFactory.CreateDAOUser();
            var user = await userDAO.GetById(commentRequestDTO.UserId);
            if (user == null)
                return BadRequest("El usuario especificado no existe.");

            // Obtener publicación
            var publishingDAO = _daoFactory.CreateDAOPublishing();
            var publishing = await publishingDAO.GetById(commentRequestDTO.PublishingId);
            if (publishing == null)
                return BadRequest("La publicación especificada no existe.");

            // Crear objeto del modelo de base de datos
            var comment = new Comment
            {
                Text = commentRequestDTO.Text,
                User = user.Name,
                PublishingId = publishing.Id,
                Date = DateTime.UtcNow
            };

            try
            {
                // Guardar el comentario en la base de datos usando el DAO
                var commentDAO = _daoFactory.CreateDAOComment();
                await commentDAO.Save(comment);

                // Crear respuesta
                var response = new CommentResponseDTO
                {
                //    Id = comment.Id,
                //    UserName = user.Name,
                    Text = comment.Text,
                //    PublishingId = publishing.Id,
                    Date = comment.Date
                };

                return CreatedAtAction(nameof(GetById), new { id = comment.Id }, response);
            }
            catch (Exception ex)
            {
                // Loguear el error y devolver respuesta de error
                _logger.LogError(ex, "Error al crear el comentario.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                // Obtener el comentario desde el DAO
                var commentDAO = _daoFactory.CreateDAOComment();
                var comment = await commentDAO.GetById(id);

                if (comment == null)
                {
                    return NotFound($"No se encontró un comentario con el ID {id}.");
                }

                // Crear respuesta
                var response = new CommentResponseDTO
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    // UserName = comment.User?.Name,
                    Date = comment.Date
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Loguear error y devolver respuesta de error
                _logger.LogError(ex, "Error al obtener el comentario.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
