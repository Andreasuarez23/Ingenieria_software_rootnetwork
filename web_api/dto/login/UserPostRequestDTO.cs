using web_api.dto.common;

namespace web_api.dto.login;

public class UserPostRequestDTO : RequestDTO
{
    public string name { get; set; } = "";
    public string lastName { get; set; } = "";
    public DateTime? birthdate { get; set; }
    public string mail { get; set; } = "";
    public string password { get; set; } = "";
}