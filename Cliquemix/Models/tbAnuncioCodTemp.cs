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
    
    public partial class tbAnuncioCodTemp
    {
        public tbAnuncioCodTemp()
        {
            this.tbAnuncioImg = new HashSet<tbAnuncioImg>();
            this.tbAnuncio = new HashSet<tbAnuncio>();
        }
    
        public int actid { get; set; }
        public Nullable<System.DateTime> dtMovimento { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<int> aid { get; set; }
    
        public virtual ICollection<tbAnuncioImg> tbAnuncioImg { get; set; }
        public virtual tbUsers tbUsers { get; set; }
        public virtual ICollection<tbAnuncio> tbAnuncio { get; set; }
        public virtual tbAnuncio tbAnuncio1 { get; set; }
    }
}
