using AparmentManager2023.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AparmentManager2023.Data
{
    public class ApplicationDBContext : IdentityDbContext<AparmentManager2023User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public ApplicationDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //Data table lists:
        public DbSet<OwnerClass> Owner { get; set; }
        public DbSet<ApartmentClass> Apartment { get; set; }
        public DbSet<PersonClass> Person { get; set; }
        public DbSet<Person_ApartmentClass> Person_Apartment { get; set; }
        public DbSet<Monthly_BillClass> Monthly_Bill { get; set; }
    }
}