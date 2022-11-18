using Microsoft.EntityFrameworkCore;
using Model;

namespace Infra;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}