using dao_library.Interfaces.reaction;
using entities_library.publishing.reactions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace dao_library.entity_framework.reaction
{
    public class DAOEFReaction : IDAOReaction
    {
        private readonly AplicationDbContext _context;

        public DAOEFReaction(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reaction> AddReaction(Reaction reaction)
        {
            // Verificar si la reacción es null
            if (reaction == null)
            {
                throw new ArgumentNullException(nameof(reaction), "Reaction cannot be null");
            }

            // Verificar si la reacción ya existe (si aplica para tu caso)
            var existingReaction = await _context.Reactions
                .FirstOrDefaultAsync(r => r.UserId == reaction.UserId && r.PostId == reaction.PostId);

            if (existingReaction != null)
            {
                // Si ya existe, puedes devolverla o realizar otra acción
                return existingReaction;
            }

            // Agregar la reacción al contexto
            _context.Reactions.Add(reaction);

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return reaction;  // Devolver la reacción recién agregada
            }
            catch (Exception ex)
            {
                // Manejar posibles errores al guardar (por ejemplo, errores de la base de datos)
                throw new InvalidOperationException("Error occurred while saving the reaction", ex);
            }
        }

        public async Task<Reaction?> GetById(long id)
        {
            // Utilizar FirstOrDefaultAsync, ya que puede devolver null si no se encuentra la reacción
            return await _context.Reactions
                .FirstOrDefaultAsync(r => r.Id == id);  // Recupera la reacción por ID
        }
    }
}
