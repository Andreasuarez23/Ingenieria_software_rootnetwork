namespace entities_library.follow;

public class Follow
{
    public long Id { get; set; } // Id de relación de seguimiento

    public long FollowerId { get; set; } // Id del usuario que sigue a otro

    public long FollowingId { get; set; } // Id del usuario que esta siendo seguido

    public bool IsFollower { get; set; } 
    
    public DateTime DateTime { get; set; } // Duración del seguimiento

}