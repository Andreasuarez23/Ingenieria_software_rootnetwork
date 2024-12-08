
using entities_library.comment;
using entities_library.file_system;
using entities_library.follow;
using entities_library.login;
using entities_library.publishing;
using entities_library.publishing.reactions;
using entities_library.report;
using Microsoft.EntityFrameworkCore;

namespace dao_library;


public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {}

    public DbSet<Comment>? Comments { get; set;}

    public DbSet<entities_library.file_system.AppFile>? Files { get; set; }

    public DbSet<Follow>? Follows { get; set;}

    public DbSet<Locked>? Locked { get; set;}

    public DbSet<Person>? Persons { get; set;}

    public DbSet<User>? Users { get; set;}

    public DbSet<Publishing>? Posts { get; set;}

    public DbSet<Report>? Report { get; set;}

    public DbSet<ReportPost>? ReportPost { get; set;}

    public DbSet<Reaction>? Reactions {get ; set;}

    public DbSet<UserBan>? UserBans { get; set;}

    public DbSet<PublishingUser>? PublishingUsers { get; set;}

}
