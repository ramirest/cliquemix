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
    
    public partial class tbDestaqueDuracao
    {
        public tbDestaqueDuracao()
        {
            this.tbDestaque = new HashSet<tbDestaque>();
        }
    
        public int ddid { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
        public string item { get; set; }
        public Nullable<double> tempo { get; set; }
        public Nullable<bool> ativo { get; set; }
    
        public virtual ICollection<tbDestaque> tbDestaque { get; set; }
    }
}
