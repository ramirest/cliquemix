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
        public Nullable<int> spna { get; set; }
        public Nullable<int> spnc { get; set; }
        public Nullable<int> tpua { get; set; }
        public Nullable<int> spadc { get; set; }
        public Nullable<int> spnac { get; set; }
    
        public virtual tbAnuncioStatus tbAnuncioStatus { get; set; }
        public virtual tbAnuncioStatus tbAnuncioStatus1 { get; set; }
        public virtual tbCampanhaStatus tbCampanhaStatus { get; set; }
        public virtual tbUsersTipo tbUsersTipo { get; set; }
        public virtual tbCampanhaAnuncioStatus tbCampanhaAnuncioStatus { get; set; }
    }
}
