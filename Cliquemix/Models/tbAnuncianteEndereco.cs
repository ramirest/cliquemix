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
    
    public partial class tbAnuncianteEndereco
    {
        public tbAnuncianteEndereco()
        {
            this.tbAnunciante1 = new HashSet<tbAnunciante>();
        }
    
        public int peid { get; set; }
        public int pid { get; set; }
        public Nullable<int> cepid { get; set; }
        public string numero_endereco { get; set; }
        public string complemento_endereco { get; set; }
    
        public virtual tbAnunciante tbAnunciante { get; set; }
        public virtual tbCep tbCep { get; set; }
        public virtual ICollection<tbAnunciante> tbAnunciante1 { get; set; }
    }
}
