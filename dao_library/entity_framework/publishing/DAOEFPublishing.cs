using dao_library.Interfaces.publishing;
using entities_library.publishing;

namespace dao_library.entity_framework.publishing;

public class DAOEFPublishing : IDAOPublishing
{
    private readonly AplicationDbContext context;

    public DAOEFPublishing(AplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(Publishing publishing) 
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Publishing>> GetAll() 
    {
        throw new NotImplementedException();
    }

    public Task<Publishing> GetById(long id) 
    {
        throw new NotImplementedException();
    }

    public Task Save(Publishing publishing) 
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Publishing>> IDAOPublishing.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<Publishing> IDAOPublishing.GetById(long id)
    {
        throw new NotImplementedException();
    }
}
