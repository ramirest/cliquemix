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
    
    public partial class tbCondicaoPagto
    {
        public tbCondicaoPagto()
        {
            this.tbAnunciante = new HashSet<tbAnunciante>();
            this.tbCalculoCondicao = new HashSet<tbCalculoCondicao>();
        }
    
        public int cpid { get; set; }
        public string descricao { get; set; }
        public int ativo { get; set; }
    
        public virtual ICollection<tbAnunciante> tbAnunciante { get; set; }
        public virtual ICollection<tbCalculoCondicao> tbCalculoCondicao { get; set; }
    }
}
