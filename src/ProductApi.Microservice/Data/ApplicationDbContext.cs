namespace ProductApi.Microservice.Data;

public class ApplicationDbContext
    : DbContext
{

    public DbSet<Product> Products { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(e => e.ProductName)
            .HasMaxLength(100);

        // Seed the data.
        // NOTE:
        //      Provide values for the Identity Column to avoid conflicts during the seeding process.
        //      The insertion script is injected into the generated Migration Code. 
        modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "First Product of Category #1", ProductCategoryId = 1},
                new Product { ProductId = 2, ProductName = "Second Product of Category #1", ProductCategoryId = 1 },
                new Product { ProductId = 3, ProductName = "Third Product - of Category #3", ProductCategoryId = 3 }
            );
    }
}
