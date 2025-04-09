using Microsoft.EntityFrameworkCore;
using DesignStudio.Data;
using DesignStudio.Data.Models;

namespace DesignStudio.Data
{
    public class DesignStudioContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }

        // Конструктор для вказівки параметрів контексту
        public DesignStudioContext(DbContextOptions<DesignStudioContext> options) : base(options) { }

        // Метод для налаштування зв'язків між сутностями
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Визначення зв'язків між сутностями

            // Зв'язок між Order і Service
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Service)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ServiceId);

            // Зв'язок між PortfolioItem і Service
            modelBuilder.Entity<PortfolioItem>()
                .HasOne(p => p.Service)
                .WithMany(s => s.PortfolioItems)
                .HasForeignKey(p => p.ServiceId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
