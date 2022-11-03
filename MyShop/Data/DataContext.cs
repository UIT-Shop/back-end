namespace MyShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.UserId, ci.ProductId });

            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.ProductColorId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<ProductColor>().HasData(
                    new { Id = "1", Name = "Default", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = "2", Name = "Paperback", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = "3", Name = "E-Book", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
                    new { Id = "4", Name = "Audiobook", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
                );

            modelBuilder.Entity<Category>().HasData(
                new
                {
                    Id = 1,
                    Name = "Books",
                    Url = "books",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    Id = 2,
                    Name = "Movies",
                    Url = "movies",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    Id = 3,
                    Name = "Video Games",
                    Url = "video-games",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );

            modelBuilder.Entity<Brand>().HasData(
                new
                {
                    Id = 1,
                    Name = "GUCCI",
                    Url = "books",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    Id = 2,
                    Name = "Channel",
                    Url = "movies",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    Id = 3,
                    Name = "Video Games",
                    Url = "video-games",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );

            modelBuilder.Entity<Product>().HasData(
                    new
                    {
                        Id = 1,
                        Title = "The Hitchhiker's Guide to the Galaxy",
                        Description = "The Hitchhiker's Guide to the Galaxy[note 1] (sometimes referred to as HG2G,[1] HHGTTG,[2] H2G2,[3] or tHGttG) is a comedy science fiction franchise created by Douglas Adams. Originally a 1978 radio comedy broadcast on BBC Radio 4, it was later adapted to other formats, including stage shows, novels, comic books, a 1981 TV series, a 1984 text-based computer game, and 2005 feature film.",
                        CategoryId = 1,
                        BrandId = 1,
                        Deleted = false,
                        Visible = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = 2,
                        Title = "Ready Player One",
                        Description = "Ready Player One is a 2011 science fiction novel, and the debut novel of American author Ernest Cline. The story, set in a dystopia in 2045, follows protagonist Wade Watts on his search for an Easter egg in a worldwide virtual reality game, the discovery of which would lead him to inherit the game creator's fortune. Cline sold the rights to publish the novel in June 2010, in a bidding war to the Crown Publishing Group (a division of Random House).[1] The book was published on August 16, 2011.[2] An audiobook was released the same day; it was narrated by Wil Wheaton, who was mentioned briefly in one of the chapters.[3][4]Ch. 20 In 2012, the book received an Alex Award from the Young Adult Library Services Association division of the American Library Association[5] and won the 2011 Prometheus Award.[6]",
                        CategoryId = 1,
                        BrandId = 1,
                        Deleted = false,
                        Visible = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = 3,
                        Title = "Nineteen Eighty-Four",
                        Description = "Nineteen Eighty-Four (also stylised as 1984) is a dystopian social science fiction novel and cautionary tale written by English writer George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime. Thematically, it centres on the consequences of totalitarianism, mass surveillance and repressive regimentation of people and behaviours within society.[2][3] Orwell, a democratic socialist, modelled the totalitarian government in the novel after Stalinist Russia and Nazi Germany.[2][3][4] More broadly, the novel examines the role of truth and facts within politics and the ways in which they are manipulated.",
                        CategoryId = 1,
                        BrandId = 1,

                        Deleted = false,
                        Visible = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = 4,
                        CategoryId = 2,
                        Title = "The Matrix",
                        Description = "The Matrix is a 1999 science fiction action film written and directed by the Wachowskis, and produced by Joel Silver. Starring Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, and Joe Pantoliano, and as the first installment in the Matrix franchise, it depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix, which intelligent machines have created to distract humans while using their bodies as an energy source. When computer programmer Thomas Anderson, under the hacker alias \"Neo\", uncovers the truth, he \"is drawn into a rebellion against the machines\" along with other people who have been freed from the Matrix.",
                        BrandId = 1,
                        Deleted = false,
                        Visible = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    }
                    );

            modelBuilder.Entity<ProductVariant>().HasData(
                new
                {
                    ProductId = 1,
                    ProductColorId = "2",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 1,
                    ProductColorId = "3",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 1,
                    ProductColorId = "4",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 2,
                    ProductColorId = "2",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 3,
                    ProductColorId = "2",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 4,
                    ProductColorId = "1",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 4,
                    ProductColorId = "2",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new
                {
                    ProductId = 4,
                    ProductColorId = "3",
                    Price = 9.99m,
                    OriginalPrice = 9.99m,
                    Deleted = false,
                    Visible = true,
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
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }
        }
    }
}
