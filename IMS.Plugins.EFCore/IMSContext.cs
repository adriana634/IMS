using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeding data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryName = "Electric Engine", Price = 1000, Quantity = 1 },
                new Inventory { InventoryId = 2, InventoryName = "Body", Price = 400, Quantity = 1 },
                new Inventory { InventoryId = 3, InventoryName = "Wheel", Price = 180, Quantity = 240 },
                new Inventory { InventoryId = 4, InventoryName = "Seat", Price = 50, Quantity = 4 },
                new Inventory { InventoryId = 5, InventoryName = "Battery", Price = 70, Quantity = 8 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Steam train", Price = 350000, Quantity = 1 },
                new Product { ProductId = 2, ProductName = "Electric train", Price = 480400, Quantity = 1 },
                new Product { ProductId = 2, ProductName = "Monorail", Price = 200400, Quantity = 1 }
            );
        }
    }
}
