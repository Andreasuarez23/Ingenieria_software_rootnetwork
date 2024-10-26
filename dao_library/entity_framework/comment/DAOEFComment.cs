using dao_library.Interfaces.comment;
using entities_library.comment;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.comment;

public class DAOEFComment : IDAOComment

{    
    private readonly AplicationDbContext context;

    public DAOEFComment(AplicationDbContext context)
    {
        this.context = context; 
    }

    public Task<IEnumerable<Comment>> GetAll()
    {
        throw new NotImplementedException();
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