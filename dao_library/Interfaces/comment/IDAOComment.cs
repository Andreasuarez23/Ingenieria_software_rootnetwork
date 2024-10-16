using entities_library.comment;

namespace dao_library.Interfaces.comment;

public interface IDAOComment
{
    Task<IEnumerable<Comment>> GetAll();
    Task Save(Comment comment);
        
    //NO PROGRAM
    Task Delete(Comment comment);
        
}