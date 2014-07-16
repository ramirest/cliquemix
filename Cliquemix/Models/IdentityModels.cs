using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Cliquemix.Models;
using System;
using System.Data.Entity.Infrastructure;

namespace Cliquemix.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser
    // class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

    }

    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("name=cliquemixEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<tbAnunciante> tbAnunciante { get; set; }
        public virtual DbSet<tbAnuncianteDestaque> tbAnuncianteDestaque { get; set; }
        public virtual DbSet<tbAnuncianteDestaqueStatus> tbAnuncianteDestaqueStatus { get; set; }
        public virtual DbSet<tbAnuncianteEndereco> tbAnuncianteEndereco { get; set; }
        public virtual DbSet<tbAnuncianteTelefone> tbAnuncianteTelefone { get; set; }
        public virtual DbSet<tbAnuncio> tbAnuncio { get; set; }
        public virtual DbSet<tbAnuncioFuncionalidade> tbAnuncioFuncionalidade { get; set; }
        public virtual DbSet<tbAnuncioImagens> tbAnuncioImagens { get; set; }
        public virtual DbSet<tbAnuncioImg> tbAnuncioImg { get; set; }
        public virtual DbSet<tbAnuncioStatus> tbAnuncioStatus { get; set; }
        public virtual DbSet<tbBairro> tbBairro { get; set; }
        public virtual DbSet<tbBan> tbBan { get; set; }
        public virtual DbSet<tbBanner> tbBanner { get; set; }
        public virtual DbSet<tbBannerImg> tbBannerImg { get; set; }
        public virtual DbSet<tbBanTipo> tbBanTipo { get; set; }
        public virtual DbSet<tbCalculoCondicao> tbCalculoCondicao { get; set; }
        public virtual DbSet<tbCalculoCondicaoPagto> tbCalculoCondicaoPagto { get; set; }
        public virtual DbSet<tbCampanha> tbCampanha { get; set; }
        public virtual DbSet<tbCampanhaAnuncio> tbCampanhaAnuncio { get; set; }
        public virtual DbSet<tbCampanhaAnuncioConsumidor> tbCampanhaAnuncioConsumidor { get; set; }
        public virtual DbSet<tbCampanhaAnuncioLocalizacao> tbCampanhaAnuncioLocalizacao { get; set; }
        public virtual DbSet<tbCampanhaAnuncioStatus> tbCampanhaAnuncioStatus { get; set; }
        public virtual DbSet<tbCampanhaStatus> tbCampanhaStatus { get; set; }
        public virtual DbSet<tbCep> tbCep { get; set; }
        public virtual DbSet<tbCidade> tbCidade { get; set; }
        public virtual DbSet<tbCondicaoPagto> tbCondicaoPagto { get; set; }
        public virtual DbSet<tbConsumidor> tbConsumidor { get; set; }
        public virtual DbSet<tbCredito> tbCredito { get; set; }
        public virtual DbSet<tbCreditoCompra> tbCreditoCompra { get; set; }
        public virtual DbSet<tbCreditoDesconta> tbCreditoDesconta { get; set; }
        public virtual DbSet<tbDestaque> tbDestaque { get; set; }
        public virtual DbSet<tbEstado> tbEstado { get; set; }
        public virtual DbSet<tbFuncionalidade> tbFuncionalidade { get; set; }
        public virtual DbSet<tbLogSistema> tbLogSistema { get; set; }
        public virtual DbSet<tbPais> tbPais { get; set; }
        public virtual DbSet<tbPermissao> tbPermissao { get; set; }
        public virtual DbSet<tbPermissaoGrupo> tbPermissaoGrupo { get; set; }
        public virtual DbSet<tbPermissaoSubGrupo> tbPermissaoSubGrupo { get; set; }
        public virtual DbSet<tbPontosRede> tbPontosRede { get; set; }
        public virtual DbSet<tbPremio> tbPremio { get; set; }
        public virtual DbSet<tbPremioConsumidor> tbPremioConsumidor { get; set; }
        public virtual DbSet<tbPremioDestaque> tbPremioDestaque { get; set; }
        public virtual DbSet<tbPremioImg> tbPremioImg { get; set; }
        public virtual DbSet<tbPremioNivel> tbPremioNivel { get; set; }
        public virtual DbSet<tbRamoAtividade> tbRamoAtividade { get; set; }
        public virtual DbSet<tbTipoLogradouro> tbTipoLogradouro { get; set; }
        public virtual DbSet<tbTos> tbTos { get; set; }
        public virtual DbSet<tbUsers> tbUsers { get; set; }
        public virtual DbSet<tbUsersLogAcesso> tbUsersLogAcesso { get; set; }
        public virtual DbSet<tbUsersPermissao> tbUsersPermissao { get; set; }
        public virtual DbSet<tbUsersTipo> tbUsersTipo { get; set; }
        public virtual DbSet<tbConfigPadrao> tbConfigPadrao { get; set; }
        public virtual DbSet<tbAnuncioImgLog> TbAnuncioImgLogs { get; set; }
    }

}
