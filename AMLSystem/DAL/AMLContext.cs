using Microsoft.EntityFrameworkCore;

namespace AMLSystem.DAL;

public class AmlContext : DbContext
{
    public AmlContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}