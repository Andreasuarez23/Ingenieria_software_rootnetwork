using entities_library.comment;



namespace dao_library.Interfaces.comment
{
    public interface IDAOComment
    {
    
        Comment Createcomment(Comment comment);

        Task Save (Comment comment);
        
        Task<Comment?> GetById(long id);

        //Task Delete (Comment comment);



    }
}
