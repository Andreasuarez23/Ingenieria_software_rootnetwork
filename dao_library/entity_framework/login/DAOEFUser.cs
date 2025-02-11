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

    public async Task<User?> GetById(long id)
    {
        return await context.Users.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontró un usuario con el ID {id}.");
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

    public async Task<(IEnumerable<User>, int)> GetAll(string? query,int page,int pageSize)
    {
        if (page <= 0 || pageSize <= 0) throw new ArgumentException("Page and pageSize must be greater than zero.");

        IQueryable<User>? usersQuery = context.Users ?? throw new InvalidOperationException("La tabla de usuarios no está disponible.");

        if (!string.IsNullOrEmpty(query))
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

    public async Task Save(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (user.Id == 0)
        {
            context.Users.Add(user); // Nuevo usuario
        }
        else
        {
            context.Users.Update(user); // Actualización
        }

        await context.SaveChangesAsync();
    }

        public async Task Update(User user)
    {
        if(user.UserStatus == UserStatus.Active)
        {
            user.UserStatus = UserStatus.Locked;
        }

        this.context.Users.Update(user);
        await this.context.SaveChangesAsync();
    }


}
