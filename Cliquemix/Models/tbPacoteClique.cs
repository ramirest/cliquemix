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
    
    public partial class tbPacoteClique
    {
        public tbPacoteClique()
        {
            this.tbCampanhaAnuncio = new HashSet<tbCampanhaAnuncio>();
        }
    
        public int pcid { get; set; }
        public string tituloPacote { get; set; }
        public string dsPacote { get; set; }
        public Nullable<int> qtdeCliques { get; set; }
    
        public virtual ICollection<tbCampanhaAnuncio> tbCampanhaAnuncio { get; set; }
    }
}
