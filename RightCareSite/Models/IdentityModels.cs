using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RightCareSite.Models.DataBase;

namespace RightCareSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<MND_TBL> MND_TBLs { get; set; }
        public DbSet<CUST_TBL> CUST_TBLs { get; set; }
        public DbSet<MND_CAT> MND_CATs { get; set; }
        public DbSet<Product_Tble> product_Tbles { get; set; }
        public DbSet<Suply_tbl> suply_Tbls { get; set; } 
        public DbSet<Category_tbl> Category_Tbls { get; set; }
        public DbSet<Stor_tbl> stor_Tbls { get; set; }
        public DbSet<Sal_tbl> Sal_Tbls { get; set; }
        public DbSet<Buy_tbl> buy_Tbls { get; set; }
        public DbSet<Gov_tbl> Gov_tbls { get; set; }
    }
}