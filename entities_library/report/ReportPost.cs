using entities_library.publishing;
namespace entities_library.report;

public class ReportPost : Report 
{
    public required Publishing PublishedReport { get; set; }
}