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
    
    public partial class tbAnuncianteStatus
    {
        public tbAnuncianteStatus()
        {
            this.tbAnunciante = new HashSet<tbAnunciante>();
            this.tbConfigPadrao = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao1 = new HashSet<tbConfigPadrao>();
        }
    
        public int ansid { get; set; }
        public string descAnuncianteStatus { get; set; }
    
        public virtual ICollection<tbAnunciante> tbAnunciante { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao1 { get; set; }
    }
}
