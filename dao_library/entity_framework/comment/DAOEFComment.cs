using entities_library.comment;
using dao_library.Interfaces.comment;

using Microsoft.EntityFrameworkCore;


namespace dao_library.entity_framework.comment
{
    public class DAOEFComment : IDAOComment
{
    private readonly AplicationDbContext _context;

    public DAOEFComment(AplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

        public Comment Createcomment(Comment comment)
        {
            throw new NotImplementedException();
        }


        public async Task Save(Comment comment)
        {
            if (comment.Id == 0)
            {
                // Es un nuevo comentario
                await _context.Set<Comment>().AddAsync(comment);
            }
            else
            {
                // Actualizar un comentario existente
                _context.Set<Comment>().Update(comment);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Comment?> GetById(long id)
        {
            return await _context.Comments
                .Include(c => c.User)         // Incluye la relación con el usuario
                .Include(c => c.PublishingId)  // Incluye la relación con la publicación
                .FirstOrDefaultAsync(c => c.Id == id);
        }






}
        
    }









    

    









    
    
