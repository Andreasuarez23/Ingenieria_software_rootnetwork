namespace dao_library.entity_framework.comment;

public class DAOEFComment : IDAOComment

{    
    private readonly AplicationDbContext context;

    public IDAOComment(AplicationDbContext context)
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
