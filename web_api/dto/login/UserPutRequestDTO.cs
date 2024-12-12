using web_api.dto.common;
namespace web_api.dto.login;
public class UserPutRequestDTO : RequestDTO
{
    public long Id { get; set; } 
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mail { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Password { get; set; }
}