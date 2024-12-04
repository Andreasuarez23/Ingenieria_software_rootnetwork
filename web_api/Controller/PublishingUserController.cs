using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using entities_library.publishing;
using dao_library.Interfaces.publishing;
using web_api.dto.publishingUser;
using System;
using System.Threading.Tasks;
using dao_library.Interfaces;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishingUserController : ControllerBase
    {
        private readonly ILogger<PublishingUserController> _logger;
        private readonly IDAOFactory _daoFactory;

        public PublishingUserController(ILogger<PublishingUserController> logger, IDAOFactory daoFactory)
        {
            _logger = logger;
            _daoFactory = daoFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PublishingUserRequestDTO publishingUserRequestDTO)
        {
            // Validar campos obligatorios
            if (string.IsNullOrEmpty(publishingUserRequestDTO.UserName))
                return BadRequest("El nombre de usuario es obligatorio.");

            if (string.IsNullOrEmpty(publishingUserRequestDTO.ImageUrl) || 
                !Uri.IsWellFormedUriString(publishingUserRequestDTO.ImageUrl, UriKind.Absolute))
                return BadRequest("La URL de la imagen no es válida.");

            // Crear objeto del modelo de base de datos
            var post = new PublishingUser
            {
                UserName = publishingUserRequestDTO.UserName,
                Description = publishingUserRequestDTO.Description,
                ImageUrl = publishingUserRequestDTO.ImageUrl,
                PublishDate = DateTime.UtcNow // Usamos la fecha actual como la de publicación
            };

            try
            {
                // Guardar el post en la base de datos usando el DAO
                var publishingUserDAO = _daoFactory.CreateDAOPublishingUser();
                await publishingUserDAO.Save(post);

                // Crear respuesta
                var response = new PublishingUserResponseDTO
                {
                    Id = post.Id,
                    UserName = post.UserName,
                    Description = post.Description,
                    PublishDate = post.PublishDate ?? DateTime.MinValue,
                    CommentsCount = 0
                };

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, response);
            }
            catch (Exception ex)
            {
                // Loguear el error y devolver respuesta de error
                _logger.LogError(ex, "Error al crear el post.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var postDAO = _daoFactory.CreateDAOPublishingUser();
                var post = await postDAO.GetById(id);

                if (post == null)
                    return NotFound($"No se encontró un post con ID {id}.");

                return Ok(new PublishingUserResponseDTO
                {
                    Id = post.Id,
                    UserName = post.UserName,
                    Description = post.Description,
                    PublishDate = post.PublishDate ?? DateTime.MinValue,
                    CommentsCount = post.Comments?.Count ?? 0
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el post.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
