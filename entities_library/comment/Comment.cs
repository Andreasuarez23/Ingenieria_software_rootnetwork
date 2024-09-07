using entities_library.login;

namespace entities_library.comment;
public class Comment{

    public long Id{get; set;}

    public required string Text {get; set;}

    public required User User  {get; set;}

    public DateTime Date {get; set;}
    
 }