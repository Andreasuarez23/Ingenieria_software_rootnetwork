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
        private readonly ILogger<ReactionController>? _logger;

        private readonly IDAOFactory?_daoFactory;

        private readonly AplicationDbContext _context; // Asegúrate de que el contexto está declarado

        public ReactionController(AplicationDbContext context) // Inyección de dependencia
        {
            _context = context;
        }


        public ReactionController(
            ILogger<ReactionController> logger,
            IDAOFactory daoFactory)
        {
            _logger = logger;
            _daoFactory = daoFactory;
        }
        





        // Endpoint para agregar una nueva reacción
        [HttpPost]
        public async Task<ActionResult<ReactionResponseDTO>> AddReaction(ReactionRequestDTO reactionRequest)
        {
            var post = await 
                this._daoFactory
                    .CreateDAOPublishing()
                    .GetById(reactionRequest.PostId);

            if (post == null )
            {
                return NotFound("Post not found");
            }
            


            var user = await 
                this._daoFactory
                .CreateDAOUser()
                .GetById(reactionRequest.UserId);
            
            if (user == null)
            {
                return NotFound("User not found");
            }

            var reaction = new Reaction
            {
        
                User = user,
                Publishing = post
            };

            _context.Reactions.Add(reaction);
            await _context.SaveChangesAsync();

            var reactionResponse = new ReactionResponseDTO
            {
                Id = reaction.Id,
                UserId = reaction.UserId,
                PostId = reaction.PostId,
                Type = reaction.Type
            };

            return CreatedAtAction(nameof(GetReaction), new { id = reactionResponse.Id }, reactionResponse);
        }

        // Endpoint para obtener una reacción por su Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ReactionResponseDTO>> GetReaction(long id)
        {
            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }

            var reactionResponse = new ReactionResponseDTO
            {
                Id = reaction.Id,
                UserId = reaction.UserId,
                PostId = reaction.PostId,
                Type = reaction.Type
            };

            return Ok(reactionResponse);
        }

        // Endpoint para obtener todas las reacciones de un post
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<ReactionResponseDTO>>> GetReactionsByPost(long postId)
        {
            var reactions = await _context.Reactions
                                          .Where(r => r.PostId == postId)
                                          .Select(r => new ReactionResponseDTO
                                          {
                                              Id = r.Id,
                                              UserId = r.UserId,
                                              PostId = r.PostId,
                                              Type = r.Type
                                          }).ToListAsync();

            return Ok(reactions);
        }

        // Endpoint para eliminar una reacción por su Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaction(long id)
        {
            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }

            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
