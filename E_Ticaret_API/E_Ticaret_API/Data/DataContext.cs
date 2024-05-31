using Microsoft.EntityFrameworkCore;

namespace E_Ticaret_API.Data
{
    public class DataContext: DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Picture> Pictures => Set<Picture>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Slider> Slider => Set<Slider>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<CouponHistory> CouponHistorys => Set<CouponHistory>();
        public DbSet<Bank> Banks => Set<Bank>();
        public DbSet<PaymentNotification> PaymentNotifications => Set<PaymentNotification>();
        public DbSet<Setting> Settings => Set<Setting>();
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<Contact> Contacts => Set<Contact>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CouponHistory>()
                .HasOne(o => o.Coupon)
                .WithMany(u => u.CouponHistorys)
                .HasForeignKey(o => o.CouponId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PaymentNotification>()
                .HasOne(o => o.Bank)
                .WithMany(u => u.PaymentNotifications)
                .HasForeignKey(o => o.BankId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CouponHistory)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CouponHistoryId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Cart>()
                .HasOne(o => o.Order)
                .WithMany(a => a.Carts)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PaymentNotification>()
                .HasOne(o => o.Order)
                .WithMany(a => a.PaymentNotifications)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
                .HasOne(o => o.Category)
                .WithMany(u => u.Products)
                .HasForeignKey(o => o.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Picture>()
                .HasOne(o => o.Product)
                .WithMany(u => u.Pictures)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
                .HasOne(o => o.Product)
                .WithMany(u => u.Comments)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
                .HasOne(o => o.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Cart>()
                .HasOne(o => o.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Cart>()
                .HasOne(o => o.Product)
                .WithMany(u => u.Carts)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}