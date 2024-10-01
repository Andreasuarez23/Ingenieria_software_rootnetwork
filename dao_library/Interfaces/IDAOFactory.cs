using dao_library.Interfaces.login;
using dao_library.Interfaces.publishing;
namespace dao_library.Interfaces;

public interface IDAOFactory 
{
    IDAOUser CreateDAOUser();

    IDAOPerson CreateDAOPerson();

    IDAOUserBan CreateDAOUserBan();
    
    IDAOPublishing CreateDAOPublishing();
}