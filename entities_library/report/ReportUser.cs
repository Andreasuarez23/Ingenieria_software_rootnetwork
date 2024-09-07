using entities_library.login;
namespace entities_library.report;

public class ReportUser : Report 
{
    public required User AssociatedUser { get; set; }
}