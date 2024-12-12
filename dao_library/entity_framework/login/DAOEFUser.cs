using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace dao_library.entity_framework.login;

public class DAOEFUser : IDAOUser
{
    private readonly AplicationDbContext context;

    public DAOEFUser(AplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetById(long id)
    {
        return await this.context.Users.FindAsync(id);
    }

    public async Task<User?> Get(string userName, string password)
    {
        if(userName == null) return null;
        if(context.Users == null) return null;

        User? user = await context.Users
            .Where(user => user.Mail.ToLower() == userName.ToLower())
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<(IEnumerable<User>, int)> GetAll(
        string? query,
        int page,
        int pageSize
    )
    {
        IQueryable<User>? usersQuery = context.Users ?? throw new InvalidOperationException("La tabla de usuarios no está disponible.");
        if (query != null)
    {
        usersQuery = usersQuery.Where(
            p => p.Mail.Contains(query) || p.Name.Contains(query));
    }

        int totalRecords = await usersQuery.CountAsync();

        var users = await usersQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalRecords);
    }

    public async Task  Save(User user)
    {
        this.context.Users.Add(user);
        await this.context.SaveChangesAsync();

    }

    public async Task Update(User user)
    {
        this.context.Users.Add(user);
        await this.context.SaveChangesAsync();
    }
}
