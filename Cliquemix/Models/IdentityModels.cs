using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Cliquemix.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser
    // class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new MySqlInitializer());            
        }

        public ApplicationDbContext(): base("DefaultConnection")
        {
        }
    }
}