using entities_library.login;

namespace entities_library.reactions;

public class Reaction {
    public long Id {get; set;}

    public required User User  { get; set;}

    public required ReactionType ReactionType {get; set;}
}