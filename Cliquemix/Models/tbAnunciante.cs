//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliquemix.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbAnunciante
    {
        public tbAnunciante()
        {
            this.tbAnuncianteDestaque = new HashSet<tbAnuncianteDestaque>();
            this.tbCreditoCompra = new HashSet<tbCreditoCompra>();
        }
    
        public int pid { get; set; }
        public string cnpj { get; set; }
        public string razaoSocial { get; set; }
        public string nmFantasia { get; set; }
        public string contato { get; set; }
        public string ie { get; set; }
        public string im { get; set; }
        public string email { get; set; }
        public string site { get; set; }
        public string obs { get; set; }
        public Nullable<int> cpid { get; set; }
        public Nullable<int> raid { get; set; }
        public Nullable<int> tid { get; set; }
        public Nullable<decimal> saldoCreditos { get; set; }
        public Nullable<int> leuTermo { get; set; }
        public Nullable<int> uid { get; set; }
        public string telResidencial { get; set; }
        public string telComercial { get; set; }
        public string telCelular1 { get; set; }
        public string telCelular2 { get; set; }
        public string numero_endereco { get; set; }
        public string complemento_endereco { get; set; }
        public string dsPais { get; set; }
        public string dsEstado { get; set; }
        public string dsCidade { get; set; }
        public string dsBairro { get; set; }
        public string dsLogradouro { get; set; }
        public string cep { get; set; }
    
        public virtual tbCondicaoPagto tbCondicaoPagto { get; set; }
        public virtual ICollection<tbAnuncianteDestaque> tbAnuncianteDestaque { get; set; }
        public virtual ICollection<tbCreditoCompra> tbCreditoCompra { get; set; }
        public virtual tbTos tbTos { get; set; }
        public virtual tbUsers tbUsers { get; set; }
        public virtual tbRamoAtividade tbRamoAtividade { get; set; }
    }
}
