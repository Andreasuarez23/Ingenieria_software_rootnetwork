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

    public async Task<(IEnumerable<UserBan>, int)> GetAll(string? query = null, int page = 1, int pageSize = 10)
    {
        // Filtrar los baneos activos (sin fecha de finalización o con fecha de finalización futura)
        IQueryable<UserBan> userBansQuery = context.UserBans
            .Include(ub => ub.User)
            .Where(userBan => userBan.EndDateTime == null || userBan.EndDateTime > DateTime.UtcNow); // Filtrar baneos activos

        if (!string.IsNullOrEmpty(query))
        {
            userBansQuery = userBansQuery.Where(userBan =>
                userBan.User.Name.Contains(query) || userBan.Reason.Contains(query));
        }

        int totalRecords = await userBansQuery.CountAsync();

        var userBans = await userBansQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (userBans, totalRecords);
    }


    public async Task<UserBan> GetById(long id)
    {
        var userBan = await this.context.UserBans.FindAsync(id);
        if (userBan == null)
        {
            throw new KeyNotFoundException($"No se encontró un baneo con el ID {id}.");
        }
        return userBan;
    }

    public async Task Save(UserBan userBan)
    {
        // Validar si el usuario ya tiene un baneo activo
        bool alreadyBanned = await context.UserBans
            .AnyAsync(ub =>
                ub.User.Id == userBan.User.Id &&
                (ub.EndDateTime == null || ub.EndDateTime > DateTime.UtcNow));

        if (alreadyBanned)
        {
            throw new InvalidOperationException("El usuario ya está baneado actualmente.");
        }

        this.context.UserBans.Add(userBan);
        await this.context.SaveChangesAsync();
    }

        public Task Delete(UserBan userBan)
    {
        throw new NotImplementedException();
    }
     public async Task Update(UserBan userBan)
    {
        throw new NotImplementedException();
    }
    public async Task Unlock(long userBanId)
    {
        var userBan = await context.UserBans.FindAsync(userBanId);

        if (userBan == null)
        {
            throw new KeyNotFoundException($"No se encontró ningún baneo con el ID {userBanId}");
        }

        if (userBan.EndDateTime.HasValue && userBan.EndDateTime > DateTime.UtcNow)
        {
            throw new InvalidOperationException("El baneo no puede ser desbloqueado porque aún no ha finalizado.");
        }

        context.UserBans.Remove(userBan); // Eliminar el baneo de la base de datos
        await context.SaveChangesAsync();
    }

    public async Task<UserBan?> GetActiveBanByUserId(long userId)
    {
        return await context.UserBans
            .Where(userBan => userBan.User.Id == userId && (userBan.EndDateTime == null || userBan.EndDateTime > DateTime.UtcNow))
            .FirstOrDefaultAsync();
    }


}
