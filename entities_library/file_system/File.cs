namespace entities_library.file_system;

public class AppFile
{
    public long Id { get; set; }

    public required string Path { get; set; }

    public FileType? FileType { get; set; }
}