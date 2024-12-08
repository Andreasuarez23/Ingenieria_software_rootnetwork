using Microsoft.AspNetCore.Mvc;
using entities_library.publishing;
using dao_library.Interfaces;
using web_api.dto.publishing;
using entities_library.login;


namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishingController : ControllerBase
    {
        private readonly ILogger<PublishingController> _logger;
        private readonly IDAOFactory _daoFactory;

        public PublishingController(ILogger<PublishingController> logger, IDAOFactory daoFactory)
        {
            _logger = logger;
            _daoFactory = daoFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PublishingRequestDTO publishingRequestDTO)
        {
            // Validar campos obligatorios
            if (string.IsNullOrEmpty(publishingRequestDTO.Text))
                return BadRequest("El nombre de usuario es obligatorio.");

            if (string.IsNullOrEmpty(publishingRequestDTO.ImageUrl) || 
                !Uri.IsWellFormedUriString(publishingRequestDTO.ImageUrl, UriKind.Absolute))
                return BadRequest("La URL de la imagen no es válida.");

            // Crear objeto del modelo de base de datos
            var post = new Publishing
            {
                //UserName = publishingRequestDTO.UserName,
                Text = publishingRequestDTO.Text,
                ImageUrl = publishingRequestDTO.ImageUrl,
                //PublishDate = DateTime.UtcNow // Usamos la fecha actual como la de publicación
            };

            try
            {
                // Guardar el post en la base de datos usando el DAO
                var publishingUserDAO = _daoFactory.CreateDAOPublishing();
                await publishingUserDAO.Save(post);

                // Crear respuesta
                var response = new PublishingResponseDTO
                {
                    Id = post.Id,
                    //UserName = post.UserName,
                    Text = post.Text,
                    ImageUrl = post.ImageUrl
                    
                    //PublishDate = post.PublishDate ?? DateTime.MinValue,
                    //CommentsCount = 0
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
                var postDAO = _daoFactory.CreateDAOPublishing();
                var post = await postDAO.GetById(id);

                if (post == null)
                    return NotFound($"No se encontró un post con ID {id}.");

                return Ok(new PublishingResponseDTO
                {
                    //Id = post.Id,
                    //UserName = post.UserName,
                    Text = post.Text,
                    ImageUrl = post.ImageUrl
                    //PublishDate = post.PublishDate ?? DateTime.MinValue,
                    //CommentsCount = post.Comments?.Count ?? 0
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
