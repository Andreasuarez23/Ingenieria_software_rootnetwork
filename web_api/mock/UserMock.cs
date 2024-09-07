using web_api.dto.login;
using entities_library.login;

namespace web_api.mock;

public class UserMock
{
    private static UserMock? instance;

    private UserMock() { }

    public static UserMock Instance 
    {
        get
        {
            if(instance == null) instance = new UserMock();
            return instance;
        }
    }

    public List<User> Users { get; set; } = new List<User>();

    public long CreateUser(
        string name, 
        string lastName, 
        string mail, 
        DateTime? birthdate, 
        string password)
    {
        User user = new User
        {
            Name = name,
            LastName = lastName,
            Mail = mail,
            Birthdate = birthdate,
            UserStatus = UserStatus.Active
        };

        user.Encrypt(password);
        user.Id = this.Users.Count + 1;
        this.Users.Add(user);

        return user.Id;
    }
}