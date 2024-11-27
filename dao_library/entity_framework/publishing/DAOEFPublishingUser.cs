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

    async Task<IEnumerable<PublishingUser>> IDAOPublishingUser.GetAll()
    {
        return await context.Set<PublishingUser>().ToListAsync();
    }

    //Task<Publishing> IDAOPublishing.GetById(long id)
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