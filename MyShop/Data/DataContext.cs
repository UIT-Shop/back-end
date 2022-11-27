namespace MyShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.UserId, ci.ProductVariantId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<ProductColor>().HasData(
                    new { Id = "Wh", Name = "Trắng", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = "Ble", Name = "Xanh dương", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = "Bla", Name = "Đen", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = "Ye", Name = "Vàng", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );
            modelBuilder.Entity<ProductType>().HasData(
                    new { Id = 1, Name = "Áo thun", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = 2, Name = "Áo khoác", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = 3, Name = "Áo sơ mi", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = 4, Name = "Quần dài", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = 5, Name = "Quần short", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = 6, Name = "Quần lót", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );

            modelBuilder.Entity<Category>().HasData(
                new
                {
                    Id = 1,
                    Name = "Áo thun tay dài",
                    Url = "ao-thun-tay-dai",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    Id = 2,
                    Name = "Áo thun tay dài",
                    Url = "ao-thun-tay-ngan",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );

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
            modelBuilder.Entity<Product>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<ProductVariant>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<ProductVariant>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<User>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<User>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<CartItem>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<CartItem>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<Order>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Order>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<OrderItem>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<OrderItem>().Property<DateTime>("UpdatedDate");

            modelBuilder.Entity<Category>().Property("Gender").HasDefaultValue("Nam");
            modelBuilder.Entity<Category>().Property("MetaTitle").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Category>().Property("MetaKeyword").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Category>().Property("MetaDiscription").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Product>().Property("MetaTitle").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Product>().Property("MetaKeyword").HasDefaultValue(String.Empty);
            modelBuilder.Entity<Product>().Property("MetaDiscription").HasDefaultValue(String.Empty);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Image> Images { get; set; }

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
