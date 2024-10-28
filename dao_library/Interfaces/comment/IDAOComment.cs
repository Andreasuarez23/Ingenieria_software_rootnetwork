using entities_library.comment;

namespace dao_library.Interfaces.comment;

public interface IDAOComment
{
    Task<IEnumerable<Comment>> GetAll(
        string? query    );
    Task Save(Comment comment);
        
    //NO PROGRAM
    Task Delete(Comment comment);
    Task GetAll(int postId);
    Task GetCommentsByPostAsync(int postId);
}