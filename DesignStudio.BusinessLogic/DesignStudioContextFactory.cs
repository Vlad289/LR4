using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DesignStudio.Data
{
    public class DesignStudioContextFactory : IDesignTimeDbContextFactory<DesignStudioContext>
    {
        public DesignStudioContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DesignStudioContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DesignStudioDB;Trusted_Connection=True;");

            return new DesignStudioContext(optionsBuilder.Options);
        }
    }
}
