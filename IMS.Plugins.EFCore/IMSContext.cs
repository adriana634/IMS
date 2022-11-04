using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore;

public sealed class IMSContext : DbContext
{
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<ProductTransaction> ProductTransactions { get; set; }

    public IMSContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Build relationships
        modelBuilder.Entity<ProductInventory>()
            .HasKey(productInventory => new { productInventory.ProductId, productInventory.InventoryId });

        modelBuilder.Entity<ProductInventory>()
            .HasOne(productInventory => productInventory.Product)
            .WithMany(product => product.ProductInventories)
            .HasForeignKey(productInventory => productInventory.ProductId);

        modelBuilder.Entity<ProductInventory>()
            .HasOne(productInventory => productInventory.Inventory)
            .WithMany(inventory => inventory.ProductInventories)
            .HasForeignKey(productInventory => productInventory.InventoryId);

        // Seeding data
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
            new Product { ProductId = 3, ProductName = "Monorail", Price = 200400, Quantity = 1 }
        );

        modelBuilder.Entity<ProductInventory>().HasData(
            new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 },
            new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 },
            new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 4 },
            new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 5 }
        );

        modelBuilder.Entity<ProductInventory>().HasData(
            new ProductInventory { ProductId = 2, InventoryId = 1, InventoryQuantity = 1 },
            new ProductInventory { ProductId = 2, InventoryId = 2, InventoryQuantity = 1 },
            new ProductInventory { ProductId = 2, InventoryId = 3, InventoryQuantity = 4 }
        );

        modelBuilder.Entity<ProductInventory>().HasData(
            new ProductInventory { ProductId = 3, InventoryId = 1, InventoryQuantity = 1 },
            new ProductInventory { ProductId = 3, InventoryId = 2, InventoryQuantity = 1 },
            new ProductInventory { ProductId = 3, InventoryId = 3, InventoryQuantity = 4 }
        );
    }
}
