using entities_library.login;

using entities_library.publishing.reactions;

namespace entities_library.publishing.reactions
{
    public class Reaction
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public ReactionType.Type ReactionType { get; set; } 
    }
}
