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
    
    public partial class tbDestaque
    {
        public tbDestaque()
        {
            this.tbAnuncianteDestaque = new HashSet<tbAnuncianteDestaque>();
            this.tbDestaqueAnunciante = new HashSet<tbDestaqueAnunciante>();
            this.tbPremioDestaque = new HashSet<tbPremioDestaque>();
        }
    
        public int did { get; set; }
        public string tituloDestaque { get; set; }
        public Nullable<double> qtCredito { get; set; }
        public double tmpEspera { get; set; }
        public string dsDestaque { get; set; }
        public string imgDestaque { get; set; }
        public Nullable<int> ddid { get; set; }
        public Nullable<double> qtDuracao { get; set; }
        public Nullable<int> pcid { get; set; }
        public Nullable<int> dsid { get; set; }
    
        public virtual ICollection<tbAnuncianteDestaque> tbAnuncianteDestaque { get; set; }
        public virtual tbDestaqueDuracao tbDestaqueDuracao { get; set; }
        public virtual ICollection<tbDestaqueAnunciante> tbDestaqueAnunciante { get; set; }
        public virtual ICollection<tbPremioDestaque> tbPremioDestaque { get; set; }
        public virtual tbDestaqueStatus tbDestaqueStatus { get; set; }
        public virtual tbPacoteClique tbPacoteClique { get; set; }
    }
}
