using dao_library.Interfaces.file;

using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.file;

public class DAOEFFile : IDAOFile
{
    private readonly AplicationDbContext context;
    
    public DAOEFFile(AplicationDbContext context)
    {
        this.context = context;
    }

    public Task Save (File file)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<File>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Delete(File file)
    {
        throw new NotImplementedException();
    }
}
