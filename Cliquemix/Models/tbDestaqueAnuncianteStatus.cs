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
    
    public partial class tbDestaqueAnuncianteStatus
    {
        public tbDestaqueAnuncianteStatus()
        {
            this.tbDestaqueAnunciante = new HashSet<tbDestaqueAnunciante>();
            this.tbConfigPadrao = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao1 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao2 = new HashSet<tbConfigPadrao>();
        }
    
        public int dasid { get; set; }
        public string dsDescricao { get; set; }
        public bool ativo { get; set; }
    
        public virtual ICollection<tbDestaqueAnunciante> tbDestaqueAnunciante { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao1 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao2 { get; set; }
    }
}
