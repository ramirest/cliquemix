using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Cliquemix.Models;

namespace Cliquemix.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser
    // class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(): base("cliquemixEntities")
        {
        }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbCep> tbCeps { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbAnuncio> tbAnuncios { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbRamoAtividade> tbRamoAtividade { get; set; }
        public System.Data.Entity.DbSet<Cliquemix.Models.tbUsersLogAcesso> TbUsersLogAcessos { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbUsers> tbUsers { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbUsersTipo> tbUsersTipo { get; set; }

    }
}