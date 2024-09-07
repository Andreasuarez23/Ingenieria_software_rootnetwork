using entities_library.login;

namespace entities_library.follow;

public class Locked {
    public long Id { get; set; }
    public required User  UserFrom { get; set; }
    public required User UserTo { get; set; }
    public required DateTime DateTime { get; set; }
}
