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

    public Task Delete(publishing publishing)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<publishing>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(pub publishing)
    {
        throw new NotImplementedException();
    }
}