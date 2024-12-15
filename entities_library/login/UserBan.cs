namespace entities_library.login;
public class UserBan
{
    public long Id{get; set;}

    public DateTime ? StartDateTime {get; set;}

    public DateTime ? EndDateTime {get; set;}

    public required User User {get; set;}

    public string? Reason {get; set;}
    
}