using entities_library.login;
using entities_library.publishing.reactions;


namespace entities_library.publishing.reactions
{
    public class Reaction
    {
        public long Id { get; set; }      
        public long UserId { get; set; }  
        public long PostId { get; set; }  
        public string Type { get; set; } = "love"; // Solo se permite "love"

        public virtual required User User { get; set; }
        public virtual required Publishing Publishing { get; set; }
    }
}

