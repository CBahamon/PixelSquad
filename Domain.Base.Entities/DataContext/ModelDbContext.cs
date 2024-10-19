using Domain.Base.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.Entities.DataContext;

public class ModelDbContext : DbContext
{
    
    public ModelDbContext(string connectionString) : base(GetOptions(connectionString)) { }

    public DbSet<Publication> Publication { get; set; }

    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publication>()
            .HasKey(p => p.Id);
    }

}