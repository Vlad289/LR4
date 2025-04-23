using Microsoft.EntityFrameworkCore;
using DesignStudio.Data.Models;

namespace DesignStudio.Data
{
    public class DesignStudioContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }

        // ✅ Цей конструктор ОБОВ’ЯЗКОВИЙ для передачі DbContextOptions
        public DesignStudioContext(DbContextOptions<DesignStudioContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Service)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ServiceId);

            modelBuilder.Entity<PortfolioItem>()
                .HasOne(p => p.Service)
                .WithMany(s => s.PortfolioItems)
                .HasForeignKey(p => p.ServiceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
