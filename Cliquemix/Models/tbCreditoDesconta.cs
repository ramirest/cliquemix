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
    
    public partial class tbCreditoDesconta
    {
        public int dcid { get; set; }
        public int adid { get; set; }
        public Nullable<double> saldoCreditoIni { get; set; }
        public Nullable<double> saldoCreditoFin { get; set; }
        public Nullable<System.DateTime> dtOcorrido { get; set; }
    
        public virtual tbAnuncianteDestaque tbAnuncianteDestaque { get; set; }
    }
}
