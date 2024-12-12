using dao_library.Interfaces.login;
using entities_library.login;

namespace dao_library.entity_framework.login;

public class DAOEFPerson : IDAOPerson
{
    private readonly AplicationDbContext context;

    public DAOEFPerson(AplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(Person person)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Person> GetById(long id)
    {
        return await this.context.Persons.FindAsync(id);
    }

    public async Task Save(Person person)
    {
        this.context.Persons.Add(person);
        await this.context.SaveChangesAsync();
    }
}