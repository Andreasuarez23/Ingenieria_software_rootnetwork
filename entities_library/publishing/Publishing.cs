using entities_library.comment;
using entities_library.login;
using entities_library.publishing.reactions;

namespace entities_library.publishing;

public class Publishing
{
  public long Id { get; set; }

    public required string Text { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();

    public List<Reaction> Reactions { get; set; } = new List<Reaction>();

    public DateTime DateTime { get; set; }

    public required User User { get; set; }

    public PublishingStatus publishingStatus { get; set; }

    public string? ImageUrl { get; set; } = "";


}