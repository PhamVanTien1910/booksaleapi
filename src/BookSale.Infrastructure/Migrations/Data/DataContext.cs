using dotnet_boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookSale.Domain.Entities;

namespace BookSale.Infrastructure.Migrations.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Entity Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FullName).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.IsSuperUser).HasColumnName("is_superuser");
                entity.Property(e => e.IsStaff).HasColumnName("is_staff");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.IsEmailConfirmed).HasColumnName("is_email_confirm");
                entity.Property(e => e.LastLogin).HasColumnName("last_login");
                entity.Property(e => e.DateJoined).HasColumnName("date_joined");

                // Set DateTimeKind to Utc for DateTime properties
                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.LastLogin).HasConversion(v => v, v => DateTime.SpecifyKind(v ?? DateTime.UtcNow, DateTimeKind.Utc));
                entity.Property(e => e.DateJoined).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

                // Make the Email property unique
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<User>().ToTable("users_user");

            // Role Entity Configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });
            modelBuilder.Entity<Role>().ToTable("roles_role");

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order Entity Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomerName).HasColumnName("customer_name");
                entity.Property(e => e.CustomerPhone).HasColumnName("customer_phone");
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("decimal(18,4)");
                entity.Property(e => e.Currency).HasColumnName("currency");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
                entity.Property(e => e.PaymentOrderId).HasColumnName("payment_order_id").IsRequired(false);
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });
            modelBuilder.Entity<Order>().ToTable("orders");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PaymentMethod)
                .WithMany()
                .HasForeignKey(o => o.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentMethod Entity Configuration
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<PaymentMethod>().ToTable("payment_methods");

            // Book Entity Configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.AuthorsId).HasColumnName("authors_id");
                entity.Property(e => e.CategorysId).HasColumnName("categorys_id");
                entity.Property(e => e.Price).HasColumnName("Price").HasColumnType("decimal(18,4)");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });
            modelBuilder.Entity<Book>().ToTable("books");

            modelBuilder.Entity<Book>()
               .HasOne(r => r.Categorys)
               .WithMany(u => u.Books)
               .HasForeignKey(u => u.CategorysId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
              .HasOne(r => r.Authors)
              .WithMany(u => u.Books)
              .HasForeignKey(u => u.AuthorsId)
              .OnDelete(DeleteBehavior.Restrict);

            // Authors Entity Configuration
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Bio).HasColumnName("bio");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<Authors>().ToTable("authors");

            // Categories Entity Configuration
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<Categories>().ToTable("categories");

            // Cart Entity Configuration
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<Cart>().ToTable("carts");

            modelBuilder.Entity<Cart>()
             .HasOne(r => r.User)
             .WithMany(u => u.Carts)
             .HasForeignKey(u => u.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
            .HasOne(r => r.Book)
            .WithMany(u => u.Carts)
            .HasForeignKey(u => u.BookId)
            .OnDelete(DeleteBehavior.Restrict);

            // Review Entity
            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.Rating).HasColumnName("rating");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });
            modelBuilder.Entity<Review>().ToTable("reviews");
            modelBuilder.Entity<Review>()
             .HasOne(r => r.User)
             .WithMany(u => u.Reviews)
             .HasForeignKey(u => u.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(u => u.Reviews)
            .HasForeignKey(u => u.BookId)
            .OnDelete(DeleteBehavior.Restrict);

            var adminRole = new Role { Id = 1, Name = "admin", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var memberRole = new Role { Id = 2, Name = "member", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var defaultPassword = BCrypt.Net.BCrypt.HashPassword("String@123");
            var user1 = new User { Id = 1, FullName = "Admin", Email = "admin@email.com", Password = defaultPassword, RoleId = 1, IsSuperUser = true, IsStaff = false, IsActive = true, IsEmailConfirmed = true, CreatedAt = DateTime.UtcNow, DateJoined = DateTime.UtcNow, LastLogin = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var user2 = new User { Id = 2, FullName = "Tien", Email = "tien@email.com", Password = defaultPassword, RoleId = 2, IsSuperUser = false, IsStaff = false, IsActive = true, IsEmailConfirmed = true, CreatedAt = DateTime.UtcNow, DateJoined = DateTime.UtcNow, LastLogin = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var paymentMethod = new PaymentMethod { Id = 1, Name = "VNPay", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };


            modelBuilder.Entity<Role>().HasData(adminRole, memberRole);
            modelBuilder.Entity<User>().HasData(user1, user2);
            modelBuilder.Entity<PaymentMethod>().HasData(paymentMethod);

            base.OnModelCreating(modelBuilder);
        }

    }
}
