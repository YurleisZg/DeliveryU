using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DeliveryU.Api.Stores.Domain.Models;
using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.Products.Domain.Models;
using DeliveryU.Api.OrdersDetails.Domain.Models;
using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Models;

namespace DeliveryU.Persistence.Database;
public class DatabaseManager : DbContext
{
    public DbSet<Person> Person { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Store> Store { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }

    public DatabaseManager(DbContextOptions<DatabaseManager> dbContext) : base(dbContext)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Id).ValueGeneratedNever();
            p.Property(p => p.Name).IsRequired();
            p.Property(p => p.LastName).IsRequired();
            p.Property(p => p.PhoneNumber).IsRequired();
            p.HasIndex(p => p.PhoneNumber).IsUnique();
            p.Property(p => p.PersonType).HasConversion(new EnumToStringConverter<PersonType>());
        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(u => u.Id);
            u.Property(u => u.Id).ValueGeneratedNever();
            u.Property(u => u.Email).IsRequired();
            u.HasIndex(u => u.Email).IsUnique();
            u.Property(u => u.Password).IsRequired();
            u.HasOne(u => u.Person).WithOne().HasForeignKey<User>("PersonId");
        });

        modelBuilder.Entity<Store>(s =>
        {
            s.HasKey(s => s.Id);
            s.Property(s => s.Id).ValueGeneratedNever();
            s.Property(s => s.Name).IsRequired();
            s.Property(s => s.Description).IsRequired();
            s.Property(s => s.ImageUrl);
        });

        modelBuilder.Entity<Product>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Id).ValueGeneratedNever();
            p.Property(p => p.Name).IsRequired();
            p.Property(p => p.Description).IsRequired();
            p.Property(p => p.Price).IsRequired();
            p.Property(p => p.Category).IsRequired();
            p.HasOne(p => p.Store).WithMany();
            p.Property(p => p.ImageUrl);
        });

        modelBuilder.Entity<Order>(o =>
        {
            o.HasKey(o => o.Id);
            o.Property(o => o.Id).ValueGeneratedNever();
            o.Property(o => o.ShippingAddress).IsRequired();
            o.Property(o => o.ShippingPrice).IsRequired();
            o.Property(o => o.TotalPrice).IsRequired();
            o.Property(o => o.DateTime).IsRequired();
            o.Property(o => o.Observations).IsRequired();
            o.HasOne(o => o.Client).WithMany();
            o.HasOne(o => o.Delivery).WithMany();
            o.Property(o => o.OrderState).HasConversion(new EnumToStringConverter<OrderState>());
            o.Property(o => o.PayMethod).HasConversion(new EnumToStringConverter<PayMethod>());
            o.Property(o => o.ClientConfirmation).HasDefaultValue(false).IsRequired();
            o.Property(o => o.DeliveryConfirmation).HasDefaultValue(false).IsRequired();
        });

        modelBuilder.Entity<OrderDetail>(od =>
        {
            od.HasKey(od => od.Id);
            od.Property(od => od.Id).ValueGeneratedNever();
            od.Property(od => od.Quantity).IsRequired();
            od.HasOne(od => od.Product).WithMany();
            od.HasOne(od => od.Order).WithMany();
        });
    }

}