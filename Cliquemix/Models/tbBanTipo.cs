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
    
    public partial class tbBanTipo
    {
        public tbBanTipo()
        {
            this.tbBan = new HashSet<tbBan>();
        }
    
        public int btid { get; set; }
        public string tituloBan { get; set; }
        public int qtTmpBan { get; set; }
    
        public virtual ICollection<tbBan> tbBan { get; set; }
    }
}