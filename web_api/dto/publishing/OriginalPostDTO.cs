public class OriginalPostDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public string? OriginalUserName { get; set; } // Usuario que hizo la publicación original
}