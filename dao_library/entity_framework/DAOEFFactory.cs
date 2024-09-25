using dao_library.Interfaces;
using dao_library.Interfaces.login;
using dao_library.entity_framework.login;
using dao_library.Interfaces.publishing;
using dao_library.entity_framework.publishing;

namespace dao_library.entity_framework;

public class DAOEFFactory : IDAOFactory
{
    private readonly AplicationDbContext context;

    public DAOEFFactory(AplicationDbContext context)
    {
        this.context = context;
    }

    public IDAOPerson CreateDAOEFPerson()
    {
        return new DAOEFPerson(context);
    }

    public IDAOPublishing CreateDAOEFPublishing()
    {
        return new DAOEFPublishing(context);
    }

    public IDAOUser CreateDAOEFUser()
    {
        return new DAOEFUser(context);
    }

    public IDAOUserBan CreateDAOEFUserBan()
    {
        return new DAOEFUserBan (context);
    }
}