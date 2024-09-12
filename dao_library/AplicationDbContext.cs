namespace dao_library;

using System;
using entities_library.comment;
using entities_library.file_system;
using entities_library.follow;
using entities_library.login;
using entities_library.publishing;
using entities_library.report;
using Microsoft.EntityFrameworkCore;

public class AplicationDbContext : DbContext

{
    public AplicationDbContext(DbContext<AplicationDbContext> options)
        : base()
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }
    }

    public DbSet<Comment>? Comments { get; }

    public DbSet<File>? Files { get; }

    public DbSet<FileType>? FileTypes { get; } /*Charlar con el grupo*/

    public DbSet<Follow>? Follows { get; }

    public DbSet<Locked>? Lockeds { get; }

    public DbSet<Person>? Persons { get; }

    public DbSet<User>? Users { get; }

    public DbSet<Publishing>? Post { get; }

    public DbSet<Report>? Reports { get; }

    public DbSet<ReportPost>? ReportsPost { get; }
}

public class DbContext<T>
{
}