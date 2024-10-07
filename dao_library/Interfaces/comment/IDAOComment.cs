namespace dao_library.Interfaces.comment;

public interface IDAOComment
{
    {
    Task<IEnumerable<comment>> GetAll();
    Task Save(comment comment);
    
    //NO PROGRAM
    Task Delete(comment comment);
}
}