using EducationApp.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Сategory> Сategories { get;  set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StructureOrder> StructureOrders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
