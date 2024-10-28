using dao_library.Interfaces.comment;
using entities_library.comment;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace dao_library.entity_framework.comment;

public class DAOEFComment : IDAOComment

{    
    private readonly AplicationDbContext context;

    public DAOEFComment(AplicationDbContext context)
    {
        this.context = context; 
    }

    public async Task<(IEnumerable<Comment>,int )GetAll(
        string?query
    )
    { 
        IQueryable<Comment>? commentsQuery = context.Comments ?? throw new InvalidOperationException("La tabla de usuarios no está disponible.");
        if (query != null) 
        {
            commentsQuery = commentsQuery.Where(
                p => p.Text.Contains(query)); 
            
        }

            int totalRecords = await commentsQuery.CountAsync();

            var comment = await commentsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (comment, totalRecords);
    }


    }




    
    
        
    public Task Save(Comment comment)
    {
        throw new NotImplementedException();
    }
    public Task Delete(Comment comment)
    {
        throw new NotImplementedException();
    }

}