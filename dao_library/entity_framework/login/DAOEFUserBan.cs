using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.EntityFrameworkCore;


namespace dao_library.entity_framework.login;

public class DAOEFUserBan : IDAOUserBan
{
    private readonly AplicationDbContext context;

    public DAOEFUserBan(AplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(UserBan userBan)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserBan>> GetAll()
    {
        return await context.UserBans.ToListAsync();
    }

    public Task<UserBan> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(UserBan userBan)
    {
        throw new NotImplementedException();
    }
}