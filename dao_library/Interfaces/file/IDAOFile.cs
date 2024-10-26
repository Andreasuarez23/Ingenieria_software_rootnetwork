using entities_library.file_system;

namespace dao_library.Interfaces.file;

public interface IDAOFile
{
    Task<IEnumerable<AppFile>> GetAll();
    Task Save(AppFile file);
        
    //NO PROGRAM
    Task Delete(AppFile file);
}