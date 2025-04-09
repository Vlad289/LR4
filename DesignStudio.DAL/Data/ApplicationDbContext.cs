using Microsoft.EntityFrameworkCore;
using DesignStudio.DAL.Models;

namespace DesignStudio.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite база буде створена автоматично поруч з .exe
            optionsBuilder.UseSqlite("Data Source=designstudio.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфігурація зв’язку багато-до-багатьох між Order і Service
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Services)
                .WithMany(s => s.Orders);
        }
    }
}

