using entities_library.comment;
using dao_library.Interfaces.comment;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.comment
{
public class DAOEFComment : IDAOComment
{
    private readonly AplicationDbContext context;

    public DAOEFComment(AplicationDbContext context)
    {
        this.context = context;
    }

    // Métodos implementados de acuerdo a la interfaz

    // Métodos que implementan la interfaz sincrónicamente
    public Comment Createcomment(Comment comment)
    {
        // Verificar que el comentario no sea nulo
        if (comment == null)
        {
            throw new ArgumentNullException(nameof(comment));
        }

        // Usar el contexto para agregar y guardar el comentario
        context.Set<Comment>().AddAsync(comment);
        context.SaveChangesAsync();

        return comment;
    }

    // Este método es asíncrono como en tu interfaz original
    public async Task Save(Comment comment)
    {
        if (comment.Id == 0)
        {
            // Es un nuevo comentario
            await context.Set<Comment>().AddAsync(comment);
        }
        else
        {
            // Actualizar un comentario existente
            context.Set<Comment>().Update(comment);
        }
        await context.SaveChangesAsync();
    }

    public async Task<Comment?> GetById(long id)
    {
        return await context.Set<Comment>()
            .Include(c => c.User)  // Incluye la relación con el usuario si existe
            .FirstOrDefaultAsync(c => c.Id == id);
    }


    public async Task Delete(long commentId)
    {
        var comment = await context.Set<Comment>().FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment != null)
        {
            context.Set<Comment>().Remove(comment);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Comentario no encontrado.");
        }
    }
}
 

}












    

    









    
    
