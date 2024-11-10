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

    public async Task<IEnumerable<PublishingUser>> GetAll()
    {
        return await _context.Set<PublishingUser>().ToListAsync();
    }


    public async Task<PublishingUser> GetById(long id)
    {
        return await _context.Set<PublishingUser>().FindAsync(id);
    }

    public async Task<PublishingUser> Save(PublishingUser publishingUser)
    {
        // Si el ID de la publicación es 0, significa que es una nueva publicación
        if (publishingUser.Id == 0)
        {
            await _context.Set<PublishingUser>().AddAsync(publishingUser);
        }
        else
        {
            _context.Set<PublishingUser>().Update(publishingUser);
        }
        
        await _context.SaveChangesAsync(); 
    }


}