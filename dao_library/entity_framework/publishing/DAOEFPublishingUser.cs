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

    public async Task<IEnumerable<PublishingUser>> GetAll()
    {
        return await context.Set<PublishingUser>().ToListAsync();
    }


    public async Task<PublishingUser> GetById(long id)
    {
        return await context.Set<PublishingUser>().FindAsync(id);
    }

    public async Task<PublishingUser> Save(PublishingUser publishingUser)
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