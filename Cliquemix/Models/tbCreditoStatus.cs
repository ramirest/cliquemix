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
    
    public partial class tbCreditoStatus
    {
        public tbCreditoStatus()
        {
            this.tbConfigPadrao = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao1 = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao2 = new HashSet<tbConfigPadrao>();
            this.tbCreditoCompra = new HashSet<tbCreditoCompra>();
        }
    
        public int crsid { get; set; }
        public string dsCreditoStatus { get; set; }
    
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao1 { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao2 { get; set; }
        public virtual ICollection<tbCreditoCompra> tbCreditoCompra { get; set; }
    }
}
