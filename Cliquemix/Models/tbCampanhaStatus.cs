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
    
    public partial class tbCampanhaStatus
    {
        public tbCampanhaStatus()
        {
            this.tbCampanha = new HashSet<tbCampanha>();
            this.tbConfigPadrao = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao1 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao2 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao3 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao4 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao5 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao6 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao7 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao8 = new HashSet<tbConfigPadrao>();
        }
    
        public int csid { get; set; }
        public string dsCampStatus { get; set; }
    
        public virtual ICollection<tbCampanha> tbCampanha { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao1 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao2 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao3 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao4 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao5 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao6 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao7 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao8 { get; set; }
    }
}
