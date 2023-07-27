using Microsoft.EntityFrameworkCore;
using TimeTable.DataBase.Models;

public class ApplicationContext : DbContext
{
    public DbSet<User> users => Set<User>();
    public DbSet<Tutor> tutors => Set<Tutor>();
    public DbSet<Pupil> pupils => Set<Pupil>();
    public DbSet<Lesson> lessons => Set<Lesson>();
    public DbSet<Group> groups => Set<Group>();
    public DbSet<Class> classes => Set<Class>();

    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=DataBase.db");
    }
}