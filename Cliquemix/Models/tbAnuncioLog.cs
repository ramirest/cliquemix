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
    
    public partial class tbAnuncioLog
    {
        public int allid { get; set; }
        public Nullable<int> aid { get; set; }
        public Nullable<int> asid { get; set; }
        public Nullable<System.DateTime> dtMovimento { get; set; }
        public Nullable<int> uid { get; set; }
        public string dsMovimento { get; set; }
        public string dsMsgError { get; set; }
        public string dsControle { get; set; }
    
        public virtual tbAnuncioStatus tbAnuncioStatus { get; set; }
        public virtual tbUsers tbUsers { get; set; }
        public virtual tbAnuncio tbAnuncio { get; set; }
    }
}
