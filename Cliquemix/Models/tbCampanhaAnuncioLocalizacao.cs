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
    
    public partial class tbCampanhaAnuncioLocalizacao
    {
        public int calid { get; set; }
        public int caid { get; set; }
        public int paid { get; set; }
        public Nullable<int> eid { get; set; }
        public Nullable<int> cid { get; set; }
    
        public virtual tbCampanhaAnuncio tbCampanhaAnuncio { get; set; }
        public virtual tbCidade tbCidade { get; set; }
        public virtual tbEstado tbEstado { get; set; }
        public virtual tbPais tbPais { get; set; }
    }
}
