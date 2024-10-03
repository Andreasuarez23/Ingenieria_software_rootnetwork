using web_api.dto.common;

namespace web_api.dto.login;

public class UserGetAllRequestDTO : RequestDTO
{
    public string? query { get; set; }= null;
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
}