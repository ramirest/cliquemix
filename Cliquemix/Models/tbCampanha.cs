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
    
    public partial class tbCampanha
    {
        public tbCampanha()
        {
            this.tbCampanhaAnuncio = new HashSet<tbCampanhaAnuncio>();
        }
    
        public int cid { get; set; }
        public string tituloCampanha { get; set; }
        public System.DateTime dtInicio { get; set; }
        public System.DateTime dtTermino { get; set; }
        public int did { get; set; }
        public int csid { get; set; }
        public Nullable<int> pid { get; set; }
        public Nullable<int> pcid { get; set; }
        public Nullable<int> ctid { get; set; }
    
        public virtual tbAnunciante tbAnunciante { get; set; }
        public virtual ICollection<tbCampanhaAnuncio> tbCampanhaAnuncio { get; set; }
        public virtual tbCampanhaStatus tbCampanhaStatus { get; set; }
        public virtual tbDestaque tbDestaque { get; set; }
        public virtual tbPacoteClique tbPacoteClique { get; set; }
        public virtual tbCampanhaTmp tbCampanhaTmp { get; set; }
    }
}
