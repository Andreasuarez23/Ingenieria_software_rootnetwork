using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using entities_library.publishing.reactions;
using System.Collections.Generic;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly ILogger<ReactionController> _logger;
        private readonly IDAOFactory _daoFactory;

        public ReactionController(ILogger<ReactionController> logger, IDAOFactory daoFactory)
        {
            _logger = logger;
            _daoFactory = daoFactory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reaction>> GetAllReactions()
        {
            // Lógica para obtener todas las reacciones
            var reactions = new List<Reaction> {
                new Reaction { Id = 1, UserId = 1, ReactionType = ReactionType.Type.like },
                new Reaction { Id = 2, UserId = 2, ReactionType = ReactionType.Type.love }
            };

            return Ok(reactions);
        }

        [HttpGet("{id}")]
        public ActionResult<Reaction> GetReactionById(long id)
        {
            // Lógica para obtener una reacción por ID
            var reaction = new Reaction { Id = id, UserId = 1, ReactionType = ReactionType.Type.like };
            return Ok(reaction);
        }

        [HttpPost]
        public ActionResult CreateReaction([FromBody] Reaction reaction)
        {
            // Lógica para crear una nueva reacción
            _logger.LogInformation("Reacción creada: {Reaction}", reaction);
            return CreatedAtAction(nameof(GetReactionById), new { id = reaction.Id }, reaction);
        }
    }
}
