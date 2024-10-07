namespace dao_library.Interfaces.file;

public interface IDAOFile
{
    {
    Task<IEnumerable<file>> GetAll();
    Task Save(file file);
    
    //NO PROGRAM
    Task Delete(file file);
}
}