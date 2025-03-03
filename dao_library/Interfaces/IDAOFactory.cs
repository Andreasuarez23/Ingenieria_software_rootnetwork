using dao_library.Interfaces.reaction;
using dao_library.Interfaces.comment;
using dao_library.Interfaces.login;
using dao_library.Interfaces.publishing;

namespace dao_library.Interfaces
{
    public interface IDAOFactory 
    {
        IDAOUser CreateDAOUser();

        IDAOPerson CreateDAOPerson();

        IDAOUserBan CreateDAOUserBan();
        
        IDAOPublishing CreateDAOPublishing();
        
        IDAOComment CreateDAOComment();

       
        IDAOReaction CreateDAOReaction();
    } 
}/*Metodos que devuelven instancias de DAO
