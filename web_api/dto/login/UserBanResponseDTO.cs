using web_api.dto.common;
namespace web_api.dto.login;

public class UserBanResponseDTO : ResponsetDTO
{
    public long Id { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Reason { get; set; }
    public long UserId { get; set; }
}
