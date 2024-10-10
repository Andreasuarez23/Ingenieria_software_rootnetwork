
namespace dao_library.Interfaces.file;

public interface IDAOFile
{
    Task<IEnumerable<file>> GetAll();
    Task Save(File file);
        
    //NO PROGRAM
    Task Delete(File file);
}