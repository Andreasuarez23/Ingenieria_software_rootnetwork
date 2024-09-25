using entities_library.login;

namespace dao_library.Interfaces.login;

public interface IDAOPerson
{
    Task<IEnumerable<Person>> GetAll();
    Task<Person> GetById(long id);
    Task Save(Person person);
    
    //NO PROGRAMAR
    Task Delete(Person person);
}