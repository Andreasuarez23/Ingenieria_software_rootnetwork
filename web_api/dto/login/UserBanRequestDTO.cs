using web_api.dto.common;
namespace web_api.dto.login;

public class UserBanRequestDTO : RequestDTO
{
    public long UserId { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Reason { get; set; }
}

