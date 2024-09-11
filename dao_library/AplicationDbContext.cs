﻿namespace dao_library;
using entities_library.comment;
using entities_library.file_system;
using entities_library.follow;
using entities_library.login;
using entities_library.publishing;
using entities_library.report;

public class AplicationDbContext : DbContext

{
    public APlicationDbcontex(DbContext<AplicationDbContext> options)
        : base(options)
    { }
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