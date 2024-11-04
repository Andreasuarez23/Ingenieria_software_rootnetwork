using entities_library.comment;



namespace dao_library.Interfaces.comment
{
    public interface IDAOComment
    {
    
        Task<(IEnumerable<Comment>, int)> GetAll(int postId, int page, int pageSize);
        Comment? GetCommentsById(int commentId);
        Comment Create(Comment comment);
        bool Delete(int commentId);
        Comment? Update(int commentId, Comment updatedComment);
    }
}
