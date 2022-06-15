using ETicaret.EntityLayer;
using Microsoft.EntityFrameworkCore;
namespace ETicaret.DataAccessLayer.CodeFirst
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var postgressql = "Server=localhost;Database=ETicaretDb;User ID=postgres;Password=1425369As;Port=5432;Integrated Security=true;Pooling=true;";
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-3IKQL6T\SQLEXPRESS;Database=GiyimOverDb;Trusted_Connection=True");
            optionsBuilder.UseSqlServer(@"Server=hostsql1.isimtescil.net;Database=pgiyimG2_dbgym3;User ID=pgiyimG2_dbgym3;Password=1425369As;Trusted_Connection=false;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().
                HasOne(x => x.Category)
                .WithMany(x => x.Products)
              .HasForeignKey(x => x.CategoryID);

            modelBuilder.Entity<BodySizes>().
                HasOne(x => x.ItemColors).
                WithMany(x => x.BodySizes)
                .HasForeignKey(x => x.ItemColorid);

            modelBuilder.Entity<ShipmenItem>().
               HasOne(x => x.Order)
               .WithMany(x => x.OrderItems)
             .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<ShipmenItem>().
            HasOne(x => x.Product)
            .WithMany(x => x.ShipmenItem)
          .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<Images>().
            HasOne(x => x.Product)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ProductID);

            modelBuilder.Entity<Adresses>().
            HasOne(x => x.users)
           .WithMany(x => x.adresses)
           .HasForeignKey(x => x.Userid);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<BodySizes> BodySizes { get; set; }
        public DbSet<ItemColors> ItemColor { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Adresses> Adresses { get; set; }
        public DbSet<Shipmens> shipmens { get; set; }
        public DbSet<ShipmenItem> shipmenItems { get; set; }
    }
}