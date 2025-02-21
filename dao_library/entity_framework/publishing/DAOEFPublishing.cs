using dao_library.Interfaces.publishing;
using entities_library.publishing;
using Microsoft.EntityFrameworkCore;

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

    public async Task<(IEnumerable<Publishing> posts, int totalRecords)> GetAll(string? query, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0) 
            throw new ArgumentException("Page and pageSize must be greater than zero.");

        // Incluir la relación con el usuario
        IQueryable<Publishing> postsQuery = context.Set<Publishing>()
                                                .Include(p => p.User); // 💡 Aquí incluimos la relación con el usuario

        if (!string.IsNullOrEmpty(query))
        {
            postsQuery = postsQuery.Where(
                p => p.Text.Contains(query) || p.ImageUrl.Contains(query));
        }

        int totalRecords = await postsQuery.CountAsync();

        var posts = await postsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (posts, totalRecords);
    }



    //public async Task<IEnumerable<Publishing>> GetAll() => await context.Set<Publishing>();


    public async Task Save(Publishing publishing) 
    {
        if (publishing.Id == 0)
        {
            // Es una nueva publicación
            await context.Set<Publishing>().AddAsync(publishing);
        }
        else
        {
            // Actualizar una publicación existente
            context.Set<Publishing>().Update(publishing);
        }
        await context.SaveChangesAsync();
    }

    async Task<Publishing> IDAOPublishing.GetById(long id)
    {
        return await context.Set<Publishing>()
        .Include(p => p.User)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

}



/*
async Task<Publishing> IDAOPublishing.Save(Publishing publishing)
    {
        if (publishing.Id == 0)
        {
            await context.Set<Publishing>().AddAsync(publishing);
        }
        else
        {
            context.Set<Publishing>().Update(publishing);
        }

        await context.Save();
        return publishing;
    }
*/