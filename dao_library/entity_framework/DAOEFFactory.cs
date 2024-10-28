using dao_library.Interfaces;
using dao_library.Interfaces.login;
using dao_library.entity_framework.login;
using dao_library.Interfaces.publishing;
using dao_library.entity_framework.publishing;
using dao_library.Interfaces.file;
using dao_library.entity_framework.file;
using dao_library.Interfaces.comment;
using dao_library.entity_framework.comment;


namespace dao_library.entity_framework;

public class DAOEFFactory : IDAOFactory
{
    private readonly AplicationDbContext context;

    public DAOEFFactory(AplicationDbContext context)
    {
        this.context = context;
    }

    public IDAOPerson CreateDAOPerson()
    {
        return new DAOEFPerson(context);
    }

    public IDAOPublishing CreateDAOPublishing()
    {
        return new DAOEFPublishing(context);
    }

    public IDAOUser CreateDAOUser()
    {
        return new DAOEFUser(context);
    }

    public IDAOUserBan CreateDAOUserBan()
    {
        return new DAOEFUserBan (context);
    }
    public IDAOComment CreateIDAOComment()
    {
        return new DAOEFComment (context);

    }
    public IDAOFile CreateDAOFile()
    {
        return new DAOEFFile(context);
    }

    public IDAOComment CreateDAOComment()
    {
        return new DAOEFComment(context);
    }




}
