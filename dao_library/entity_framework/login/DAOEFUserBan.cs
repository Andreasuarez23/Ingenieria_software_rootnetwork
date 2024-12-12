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

    public async Task<UserBan> GetById(long id)
    {
         return await this.context.UserBans.FindAsync(id);
    }

    public async Task Save(UserBan userBan)
    {
        this.context.UserBans.Add(userBan);
        await this.context.SaveChangesAsync();
    }
}