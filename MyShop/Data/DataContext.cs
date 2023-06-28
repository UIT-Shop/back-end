namespace MyShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.UserId, ci.ProductVariantId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductColor, oi.ProductSize });

            modelBuilder.Entity<RatingPerProduct>()
                .HasKey(r => new { r.UserId, r.ProductId });

            modelBuilder.Entity<Brand>().HasData(
                new
                {
                    Id = 1,
                    Name = "UNIQLO",
                    Url = "uniqlo",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );

            modelBuilder.Entity<Product>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Product>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<ProductVariant>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<ProductVariant>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<ProductVariantStore>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<ProductVariantStore>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<Warehouse>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Warehouse>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<User>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<User>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<CartItem>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<CartItem>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<Order>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Order>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<OrderItem>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<OrderItem>().Property<System.Nullable<DateTime>>("UpdatedDate");
            modelBuilder.Entity<Comment>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Comment>().Property<System.Nullable<DateTime>>("UpdatedDate");

            modelBuilder.Entity<Category>().Property("Gender").HasDefaultValue("Nam");
            modelBuilder.Entity<Category>().Property("MetaTitle").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Category>().Property("MetaKeyword").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Category>().Property("MetaDiscription").HasDefaultValue(String.Empty);

            modelBuilder.Entity<Product>().Property("MetaTitle").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Product>().Property("MetaKeyword").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Product>().Property("MetaDiscription").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Product>().Property("Visible").HasDefaultValue(true);
            modelBuilder.Entity<Product>().Property("Deleted").HasDefaultValue(false);
            modelBuilder.Entity<Product>().Property("CreatedDate").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Product>().Property("Rating").HasDefaultValue(5f);

            modelBuilder.Entity<ProductVariant>().Property("CreatedDate").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<ProductVariant>().Property("Visible").HasDefaultValue(true);
            modelBuilder.Entity<ProductVariant>().Property("Deleted").HasDefaultValue(false);

            modelBuilder.Entity<Sale>().Property("Totals").HasDefaultValue(0);

            modelBuilder.Entity<User>().Property("Deleted").HasDefaultValue(false);
            modelBuilder.Entity<User>().Property("Role").HasDefaultValue(Role.Customer);
            modelBuilder.Entity<User>().Property("Phone").HasDefaultValue(String.Empty);
            modelBuilder.Entity<User>().Property("Height").HasDefaultValue(150);
            modelBuilder.Entity<User>().Property("Weight").HasDefaultValue(50);
            modelBuilder.Entity<User>().Property("IsEmailVerified").HasDefaultValue(false);
            modelBuilder.Entity<User>().Property("AddressId").HasDefaultValue(null);
            modelBuilder.Entity<User>().Property("PasswordHash").HasDefaultValue(null);
            modelBuilder.Entity<User>().Property("PasswordSalt").HasDefaultValue(null);
            modelBuilder.Entity<User>().Property("CreatedDate").HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<ProductVariantStore>().Property("ProductVariantId").HasDefaultValue(null);
            modelBuilder.Entity<ProductVariantStore>().Property("WarehouseId").HasDefaultValue(null);
            modelBuilder.Entity<ProductVariantStore>().Property("Stock").HasDefaultValue(0);
            modelBuilder.Entity<ProductVariantStore>().Property("CreatedDate").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<ProductVariantStore>().Property("Deleted").HasDefaultValue(false);

            modelBuilder.Entity<Warehouse>().Property("Deleted").HasDefaultValue(false);
            modelBuilder.Entity<Warehouse>().Property("Name").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Warehouse>().Property("Phone").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Warehouse>().Property("AddressId").HasDefaultValue(null);
            modelBuilder.Entity<Warehouse>().Property("CreatedDate").HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Order>().Property("Name").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Order>().Property("Phone").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Order>().Property("CreatedDate").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Order>().Property("OrderDate").HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<OrderItem>().Property("ProductSize").HasDefaultValue(String.Empty);
            modelBuilder.Entity<OrderItem>().Property("ProductColor").HasDefaultValue(String.Empty);
            modelBuilder.Entity<OrderItem>().Property("CreatedDate").HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Comment>().Property("ProductSize").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Comment>().Property("ProductColor").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Comment>().Property("ProductTitle").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Comment>().Property("UserName").HasDefaultValue(String.Empty);
            modelBuilder.Entity<OrderItem>().Property("CreatedDate").HasDefaultValue(DateTime.Now);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantStore> ProductVariantStores { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageComment> ImageComments { get; set; }
        public DbSet<RatingPerProduct> Ratings { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            try
            {
                var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

                foreach (var entityEntry in entries)
                {
                    foreach (var property in entityEntry.Properties)
                    {
                        if (property.Metadata.Name == "UpdatedDate")
                        {
                            entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                        }
                        if (entityEntry.State == EntityState.Added && property.Metadata.Name == "CreatedDate")
                        {
                            entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

    }
}
