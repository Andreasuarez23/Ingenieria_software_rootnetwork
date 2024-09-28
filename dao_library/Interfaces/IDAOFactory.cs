using dao_library.Interfaces.login;
using dao_library.Interfaces.publishing;
namespace dao_library.Interfaces;

public interface IDAOFactory 
{
    IDAOUser CreateDAOEFUser();

    IDAOPerson CreateDAOEFPerson();

    IDAOUserBan CreateDAOEFUserBan();
    
    IDAOPublishing CreateDAOEFPublishing();
}