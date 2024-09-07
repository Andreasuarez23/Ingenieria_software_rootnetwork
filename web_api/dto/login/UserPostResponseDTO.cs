using web_api.dto.common;

namespace web_api.dto.login;

public class UserPostResponseDTO : ResponsetDTO
{
    public long id { get; set; }
    public string name { get; set; } = "";
    public string lastName { get; set; } = "";
    public string mail { get; set; } = "";
}