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
    
    public partial class tbLogMovFinanceiro
    {
        public int lmfid { get; set; }
        public Nullable<int> uid { get; set; }
        public System.DateTime dataHoraLog { get; set; }
        public decimal vlrAtual { get; set; }
        public decimal vlrMovimento { get; set; }
        public int tipoMov { get; set; }
        public Nullable<int> id { get; set; }
        public string tb { get; set; }
    
        public virtual tbUsers tbUsers { get; set; }
    }
}
