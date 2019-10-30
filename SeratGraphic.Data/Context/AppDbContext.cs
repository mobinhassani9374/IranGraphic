using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SeratGraphic.DomainModels.Entities;
using System.Linq;

namespace SeratGraphic.Data.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = builder.Entity<User>();
            user.ToTable("Users", "dbo");

            user.Property(c => c.PhoneNumber).IsRequired(true);
            user.Property(c => c.FullName).HasMaxLength(250).IsRequired(true);

            builder.Entity<IdentityRole>().ToTable("Roles","dbo");

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "dbo");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "dbo");

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "dbo");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "dbo");

            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "dbo");

            //product
           var product= builder.Entity<Product>();

            product.HasKey(c => c.Id);

            product.Property(c => c.Title).HasMaxLength(250).IsRequired(true);

            product.Property(c => c.Description).HasMaxLength(1000).IsRequired(true);

            product.Property(c => c.Extension).HasMaxLength(25).IsRequired(true);

            product.Property(c => c.Image).HasMaxLength(100);

            product.Property(c => c.SmallImage).HasMaxLength(100);

            product.Property(c => c.File).HasMaxLength(100);

            product.Property(c => c.TagJson).HasMaxLength(10000);

            product.Property(c => c.Dimensions).HasMaxLength(100);

            product
                .HasOne(c => c.User)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
