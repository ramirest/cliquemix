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
    
    public partial class tbConsumidor
    {
        public tbConsumidor()
        {
            this.tbCampanhaAnuncioConsumidor = new HashSet<tbCampanhaAnuncioConsumidor>();
        }
    
        public int cid { get; set; }
        public int uid { get; set; }
        public Nullable<int> leuTermo { get; set; }
        public int tid { get; set; }
    
        public virtual ICollection<tbCampanhaAnuncioConsumidor> tbCampanhaAnuncioConsumidor { get; set; }
        public virtual tbTos tbTos { get; set; }
    }
}
