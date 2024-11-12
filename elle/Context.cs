using System.Configuration;
using elle.Models;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;
    public DbSet<Home> Homes { get; set; } = default!;
    public DbSet<Immovable> Immovables { get; set; } = default!;

    public Context(DbContextOptions<Context> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["env"]
                .ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
