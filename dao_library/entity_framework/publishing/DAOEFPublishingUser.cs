using dao_library;
using dao_library.Interfaces.publishing;
using entities_library.publishing;
using Microsoft.EntityFrameworkCore;

namespace entity_framework.publishing;


public class DAOEFPublishingUser : IDAOPublishingUser
{
    private readonly AplicationDbContext context;

    public DAOEFPublishingUser(AplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<(List<PublishingUser>, int)> GetAll(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new ArgumentException("El número de página y el tamaño de página deben ser mayores que 0.");

        // Contar el total de elementos
        var totalRecords = await context.Set<PublishingUser>().CountAsync();

        // Obtener la lista paginada
        var data = await context.Set<PublishingUser>()
            //.OrderByDescending(p => p.PublishDate) // Ordenar por fecha de publicación
            .Skip((pageNumber - 1) * pageSize) // Saltar los registros de páginas anteriores
            .Take(pageSize) // Tomar el número de registros necesarios
            .ToListAsync();

        return (data, totalRecords);
    }

    async Task<PublishingUser?> IDAOPublishingUser.GetById(long id)
    {
        var user = await context.Set<PublishingUser>().FindAsync(id);
        if (user == null)
        {
            throw new Exception("Usuario no encontrado");
        }
        return user;
    }

    async Task<PublishingUser> IDAOPublishingUser.Save(PublishingUser publishingUser)
    {
        if (publishingUser.Id == 0)
        {
            await context.Set<PublishingUser>().AddAsync(publishingUser);
        }
        else
        {
            context.Set<PublishingUser>().Update(publishingUser);
        }

        await context.SaveChangesAsync();
        return publishingUser;
    }



}