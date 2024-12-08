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

    public async Task AddPost(Publishing publishing)
    {
        await context.Set<Publishing>().AddAsync(publishing);
        await context.SaveChangesAsync();
    }

    public Task Delete(Publishing publishing) 
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Publishing>> GetAll()
    {
        throw new NotImplementedException();
    }

    //public async Task<IEnumerable<Publishing>> GetAll() => await context.Set<Publishing>();

    public Task<Publishing> GetById(long id) 
    {
        throw new NotImplementedException();
    }

    public Task Save(Publishing publishing) 
    {
        throw new NotImplementedException();
    }

    Task<Publishing> IDAOPublishing.GetById(long id)
    {
        throw new NotImplementedException();
    }
}
