using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using entities_library.publishing.reactions;
using System.Threading.Tasks;
using dao_library;
using entities_library.publishing;
using Microsoft.EntityFrameworkCore;

namespace web_api.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly ILogger<ReactionController> _logger;
        private readonly IDAOFactory daoFactory;

        public ReactionController(
            ILogger<ReactionController> logger, 
            IDAOFactory daoFactory)
        {
            _logger = logger;
            this.daoFactory = daoFactory;
        }

        [HttpPost]
        public async Task<ActionResult<ReactionResponseDTO>> AddReaction(ReactionRequestDTO reactionRequest)
        {
            var reactionDAO = daoFactory.CreateDAOReaction();

            // Supongamos que tienes servicios o DAOs para cargar User y Publishing
            var userDAO = daoFactory.CreateDAOUser();
            var publishingDAO = daoFactory.CreateDAOPublishing();

            var user = await userDAO.GetById(reactionRequest.UserId);
            var publishing = await publishingDAO.GetById(reactionRequest.PostId);

            if (user == null || publishing == null)
            {
                return NotFound("User or Publishing not found");
            }

            var reaction = new Reaction
            {
                UserId = reactionRequest.UserId,
                PostId = reactionRequest.PostId,
                Type = reactionRequest.Type,
                User = user, // Inicializa la propiedad requerida
                Publishing = publishing // Inicializa la propiedad requerida
            };

            var addedReaction = await reactionDAO.AddReaction(reaction);
            if (addedReaction == null)
            {
                return NotFound("Unable to add reaction");
            }

            return CreatedAtAction(nameof(GetReaction), new { id = addedReaction.Id }, new ReactionResponseDTO
            {
                Id = addedReaction.Id,
                UserId = addedReaction.UserId,
                PostId = addedReaction.PostId,
                Type = addedReaction.Type
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReactionResponseDTO>> GetReaction(long id)
        {
            var reactionDAO = daoFactory.CreateDAOReaction();
            var reaction = await reactionDAO.GetById(id);

            // Si no se encuentra la reacción, devolver NotFound
            if (reaction == null)
            {
                return NotFound("Reaction not found");
            }

            // Devolver la reacción como respuesta
            return Ok(new ReactionResponseDTO
            {
                Id = reaction.Id,
                UserId = reaction.UserId,
                PostId = reaction.PostId,
                Type = reaction.Type
            });
        }



    }
}