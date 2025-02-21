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
            if (string.IsNullOrEmpty(publishingRequestDTO.Text))
                return BadRequest(new
                {
                    success = false,
                    message = "El texto de la publicación es obligatorio."
                });

            if (string.IsNullOrEmpty(publishingRequestDTO.ImageUrl) ||
                !Uri.IsWellFormedUriString(publishingRequestDTO.ImageUrl, UriKind.Absolute))
                return BadRequest(new
                {
                    success = false,
                    message = "La URL de la imagen no es válida."
                });

            User user = await this._daoFactory.CreateDAOUser().GetById(publishingRequestDTO.UserId);

            var post = new Publishing
            {
                Text = publishingRequestDTO.Text,
                ImageUrl = publishingRequestDTO.ImageUrl,
                User = user,
                publishingStatus = PublishingStatus.Published,
                DateTime = DateTime.UtcNow
            };

            try
            {
                var publishingUserDAO = _daoFactory.CreateDAOPublishing();
                await publishingUserDAO.Save(post);

                var response = new PublishingResponseDTO
                {
                    Id = post.Id,
                    UserName = user.Name,
                    Text = post.Text,
                    ImageUrl = post.ImageUrl
                };

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, new
                {
                    success = true,
                    message = "Publicación creada con éxito.",
                    data = response
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el post.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al crear la publicación.",
                    error = ex.Message
                });
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
                    ImageUrl = post.ImageUrl,
                    PublishDate = post.DateTime
                    //PublishDate = post.PublishDate ?? DateTime.MinValue
                    //CommentsCount = post.Comments?.Count ?? 0
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el post.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllPosts(string? query, int page = 1, int pageSize = 10)
        {
            try
            {
                var postDAO = _daoFactory.CreateDAOPublishing();
                var (posts, totalRecords) = await postDAO.GetAll(query, page, pageSize);

                if (!posts.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        message = "No se encontraron publicaciones.",
                        data = new List<object>(),
                        totalRecords = totalRecords
                    });
                }

                var response = posts.Select(post => new PublishingResponseDTO
                {
                    Id = post.Id,
                    UserName = post.User?.Name,
                    Text = post.Text,
                    ImageUrl = post.ImageUrl,
                    PublishDate = post.DateTime
                }).ToList();

                return Ok(new
                {
                    success = true,
                    message = "Publicaciones obtenidas con éxito.",
                    data = response,
                    totalRecords = totalRecords // Incluye el total de registros
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las publicaciones.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al obtener las publicaciones.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetPostsByUser/{id_user}")]
        public async Task<IActionResult> GetPostsByUser(int id_user)
        {
            try
            {
                var userDAO = _daoFactory.CreateDAOUser();
                var postDAO = _daoFactory.CreateDAOPublishing();

                // Obtener el usuario por ID
                var user = await userDAO.GetById(id_user);
                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                // Obtener todas las publicaciones (incluyendo las compartidas)
                var (posts, totalRecords) = await postDAO.GetAll(null, 1, 10);

                var userPosts = posts
                    .Where(p => p.User != null && p.User.Id == id_user) // Filtrar solo las publicaciones del usuario
                    .Select(p => new 
                    {
                        p.Id,
                        p.Text,
                        p.ImageUrl,
                        p.DateTime,
                        IsShared = p.OriginalPost != null, // Indica si es una publicación compartida
                        OriginalPost = p.OriginalPost != null 
                            ? new 
                            {
                                p.OriginalPost.Id,
                                p.OriginalPost.Text,
                                p.OriginalPost.ImageUrl,
                                p.OriginalPost.DateTime,
                                OriginalUser = new 
                                {
                                    p.OriginalPost.User.Id,
                                    p.OriginalPost.User.Name
                                }
                            } 
                            : null
                    })
                    .ToList();

                return Ok(new
                {
                    User = new
                    {
                        user.Id,
                        user.Name,
                        user.Mail
                    },
                    Posts = userPosts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las publicaciones del usuario.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al obtener las publicaciones del usuario.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("share")]
    public async Task<IActionResult> SharePost([FromBody] SharePostDTO sharePostDTO)
    {
        try
        {
            var userDAO = _daoFactory.CreateDAOUser();
            var postDAO = _daoFactory.CreateDAOPublishing();

            // Verificar que el usuario existe
            var user = await userDAO.GetById(sharePostDTO.UserId);
            if (user == null)
            {
                return NotFound(new { success = false, message = "Usuario no encontrado." });
            }

            // Verificar que la publicación original existe
            var originalPost = await postDAO.GetById(sharePostDTO.PostId);
            if (originalPost == null)
            {
                return NotFound(new { success = false, message = "Publicación original no encontrada." });
            }

            // Crear una nueva publicación que referencia a la original
            var sharedPost = new Publishing
            {
                Text = originalPost.Text, // Se mantiene el mismo texto
                ImageUrl = originalPost.ImageUrl, // Se mantiene la misma imagen
                User = user, // Usuario que comparte
                OriginalPost = originalPost, // Referencia a la publicación original
                publishingStatus = PublishingStatus.Published,
                DateTime = DateTime.UtcNow
            };

            await postDAO.Save(sharedPost);

            return Ok(new
            {
                success = true,
                message = "Publicación compartida con éxito.",
                sharedPostId = sharedPost.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al compartir la publicación.");
            return StatusCode(500, new
            {
                success = false,
                message = "Ocurrió un error inesperado al compartir la publicación.",
                error = ex.Message
            });
        }
    }

  }

}
