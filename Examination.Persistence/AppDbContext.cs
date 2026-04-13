using Microsoft.EntityFrameworkCore;

namespace Examination.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Test> Tests { get; init; }
    public DbSet<Question> Questions { get; init; }
    public DbSet<Option> Options { get; init; }
    public DbSet<UserAttempt> UserAttempts { get; init; }
    public DbSet<UserAnswer> UserAnswers { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var t = typeof(AppDbContext);
        modelBuilder.ApplyConfigurationsFromAssembly(t.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}