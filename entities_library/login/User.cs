namespace entities_library.login;


public class User : Person
{
    #region Atributtes
    public string Password { get; set; } = "";

    public UserStatus UserStatus { get; set; } = UserStatus.Active;

    public entities_library.file_system.AppFile? File { get; set; }

    public string Description { get; set; } = "";

    public bool IsAdmin {get; set;}= false;

    #endregion

    #region Methods
    public void Encrypt(string password)
    {
        this.Password = this.encrypt(password);
    } 

    public bool IsPassword(string password)
    {
        return this.encrypt(password) == this.Password;
    }

    private string encrypt(string password)
    {
        return password;
    }
    #endregion
}