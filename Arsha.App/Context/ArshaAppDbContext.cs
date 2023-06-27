using Arsha.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Context
{
    public class ArshaAppDbContext:DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public ArshaAppDbContext(DbContextOptions<ArshaAppDbContext> options):base(options)
        {
            
        }
    }
}
