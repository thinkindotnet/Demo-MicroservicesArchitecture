using CategoryApi.Microservice.DomainModels;

namespace CategoryApi.Microservice.Data;


public class ApplicationDbContext 
    : DbContext
{

    public DbSet<Category> Categories { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Category>(entity =>
        {
            // change fieldtype to VARCHAR (default is NVARCHAR)
            entity.Property(e => e.CategoryName)
                  .IsUnicode(false);
        });


        // Seed the data.
        // NOTE:
        //      Provide values for the Identity Column to avoid conflicts during the seeding process.
        //      The insertion script is injected into the generated Migration Code. 
        modelBuilder.Entity<Category>()
            .HasData(
                new Category { CategoryId = 1, CategoryName = "First Category" },
                new Category { CategoryId = 2, CategoryName = "Second Category" },
                new Category { CategoryId = 3, CategoryName = "Third Category" },
                new Category { CategoryId = 4, CategoryName = "Fourth Category" },
                new Category { CategoryId = 5, CategoryName = "Fifth Category" }
            );

    }
}
