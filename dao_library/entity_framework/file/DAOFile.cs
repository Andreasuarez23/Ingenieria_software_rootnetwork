using dao_library.Interfaces.file;
using entities_library.file_system;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.file;

public class DAOEFFile : IDAOFile
{
    private readonly AplicationDbContext context;
    
    public DAOEFFile(AplicationDbContext context)
    {
        this.context = context;
    }

    public Task Save (AppFile file)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppFile>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Delete(AppFile file)
    {
        throw new NotImplementedException();
    }
}
