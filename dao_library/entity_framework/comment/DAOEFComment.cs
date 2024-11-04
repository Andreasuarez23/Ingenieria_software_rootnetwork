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

        public Comment Create(Comment comment)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<Comment>, int)> GetAll(int postId, int page, int pageSize)
    {
        if (_context.Comments == null)
        {
            throw new InvalidOperationException("Comments no está definido en el contexto.");
        }

        var commentsQuery = _context.Comments.Where(c => c.PostId == postId);
        int totalRecords = await commentsQuery.CountAsync();

        var comments = await commentsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (comments, totalRecords);
    }

        public Comment? GetCommentsById(int commentId)
        {
            throw new NotImplementedException();
        }

        public Comment? Update(int commentId, Comment updatedComment)
        {
            throw new NotImplementedException();
        }

        // Otros métodos...
    }

}







    

    









    
    
