﻿using Microsoft.AspNet.Identity.EntityFramework;
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

        public System.Data.Entity.DbSet<Cliquemix.Models.tbAnunciante> tbAnunciantes { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbCondicaoPagto> tbCondicaoPagto { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbRamoAtividade> tbRamoAtividade { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbTos> tbTos { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbUsers> tbUsers { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbAnuncianteDestaque> tbAnuncianteDestaques { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbAnuncianteDestaqueStatus> tbAnuncianteDestaqueStatus { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbDestaque> tbDestaque { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbPremioNivel> tbPremioNivels { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbPremio> tbPremios { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbUsersTipo> tbUsersTipoes { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbAnuncio> tbAnuncios { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbAnuncioArea> tbAnuncioArea { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbBanner> tbBanners { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbBan> tbBans { get; set; }

        public System.Data.Entity.DbSet<Cliquemix.Models.tbBanTipo> tbBanTipo { get; set; }
    }
}