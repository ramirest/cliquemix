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
    
    public partial class tbPremioConsumidor
    {
        public int pcid { get; set; }
        public int prid { get; set; }
        public int cacid { get; set; }
        public System.DateTime dtOcorrido { get; set; }
    
        public virtual tbCampanhaAnuncioConsumidor tbCampanhaAnuncioConsumidor { get; set; }
        public virtual tbPremio tbPremio { get; set; }
    }
}
