using Microsoft.AspNetCore.Mvc;
using entities_library.publishing;
using dao_library.Interfaces.publishing;
using dao_library.Interfaces;
using dao_library.entity_framework;
using web_api.dto.publishingUser;
using web_api.dto.comment;
using web_api.dto;
using dao_library.Interfaces.comment;
using entities_library.comment;

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
            if (string.IsNullOrEmpty(publishingUserRequestDTO.UserName) || publishingUserRequestDTO.Content == null)
                return BadRequest("Datos inválidos.");

            var post = new PublishingUser
            {
                UserName = publishingUserRequestDTO.UserName,
                Description = publishingUserRequestDTO.Description,
                Content = publishingUserRequestDTO.Content,
                PublishDate = DateTime.UtcNow
            };

            try
            {
                var publishingUserDAO = _daoFactory.CreateDAOPublishingUser();
                await publishingUserDAO.Save(post);

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, new PublishingUserResponseDTO
                {
                    Id = post.Id,
                    UserName = post.UserName,
                    Description = post.Description,
                    PublishDate = post.PublishDate ?? DateTime.MinValue,
                    CommentsCount = 0
                });
            }
            catch (Exception ex)
            {
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
                    PublishDate = post.PublishDate.HasValue ? post.PublishDate.Value : DateTime.MinValue,
                    CommentsCount = post.Comments?.Count ?? 0 // Cálculo del conteo
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