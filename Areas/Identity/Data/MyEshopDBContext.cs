using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyEshop.Areas.Identity.Data;
using MyEshop.Models;

namespace MyEshop.Areas.Identity.Data;

public class MyEshopDBContext : IdentityDbContext<MyEshopUser>
{
    public MyEshopDBContext(DbContextOptions<MyEshopDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        base.OnModelCreating(builder);
        builder.Entity<Employee>().ToTable("Employee");
        builder.Entity<Products>().ToTable("Products");
        builder.Entity<Orders>().ToTable("Orders");
        builder.Entity<OrderDetails>().ToTable("OrderDetails");
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new MyEshopUserEntityConfiguration());
    }

    internal object Find(int? id)
    {
        throw new NotImplementedException();
    }

    public DbSet<Employee> Employee { get; set; }
    public DbSet<Products> Products { get; set; }

    public DbSet<Orders> Orders { get; set; }

    public DbSet<OrderDetails> OrderDetails { get; set; }
}

public class MyEshopUserEntityConfiguration : IEntityTypeConfiguration<MyEshopUser>
{
    public MyEshopUserEntityConfiguration()
    {

    }

    public void Configure(EntityTypeBuilder<MyEshopUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}