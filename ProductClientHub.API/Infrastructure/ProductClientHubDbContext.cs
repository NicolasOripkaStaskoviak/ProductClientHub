using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Entities;

namespace ProductClientHub.API.Infrastructure
{
    public class ProductClientHubDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Substituir futuramente
            optionsBuilder.UseSqlite("Data Source=\"C:\\Users\\paulo\\source\\repos\\NicolasOripkaStaskoviak\\ProductClientHub\\ProductClientHub.API\\Infrastructure\\projectdb.db\"");
        }

    }
}
