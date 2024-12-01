using entities_library.login;
using entities_library.publishing;

namespace entities_library.comment;

public class Comment
{
    public long Id { get; set; }

    public required string Text { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int PostId { get; set; }

    // Relación con PublishingUser
    public virtual PublishingUser PublishingUser { get; set; }
}
