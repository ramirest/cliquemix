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
    
    public partial class tbDestaqueStatus
    {
        public tbDestaqueStatus()
        {
            this.tbDestaque = new HashSet<tbDestaque>();
            this.tbConfigPadrao = new HashSet<tbConfigPadrao>();
            this.tbConfigPadrao1 = new HashSet<tbConfigPadrao>();
        }
    
        public int dsid { get; set; }
        public string dsDestaqueStatus { get; set; }
    
        public virtual ICollection<tbDestaque> tbDestaque { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao { get; set; }
        public virtual ICollection<tbConfigPadrao> tbConfigPadrao1 { get; set; }
    }
}