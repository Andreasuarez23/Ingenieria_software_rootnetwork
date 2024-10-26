namespace entities_library.publishing.reactions
{
    public class ReactionType
    {
        public long Id  { get; set; } 
        
        public enum Type
        {
            like,
            love
        }
    }
}

