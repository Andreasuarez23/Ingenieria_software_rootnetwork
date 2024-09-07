using entities_library.login;

namespace entities_library.report;

public class Report {
    
    public long Id { get; set;}

    public required User User { get; set; }

    public required ReportStatus ReportStatus { get; set;}

    private DateTime dateTime;

    public DateTime GetDateTime()
    {
        return dateTime;
    }

    public void SetDateTime(DateTime value)
    {
        dateTime = value;
    }

    public required DateTime DateTime { get; set; }

    public required string Reason { get; set; }

}