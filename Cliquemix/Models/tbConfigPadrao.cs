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
    
    public partial class tbConfigPadrao
    {
        public int cfgid { get; set; }
        public Nullable<int> asid { get; set; }
        public Nullable<int> utid { get; set; }
    
        public virtual tbAnuncioStatus tbAnuncioStatus { get; set; }
        public virtual tbUsersTipo tbUsersTipo { get; set; }
    }
}
