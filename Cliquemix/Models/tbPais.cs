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
    
    public partial class tbPais
    {
        public tbPais()
        {
            this.tbCampanhaAnuncioLocalizacao = new HashSet<tbCampanhaAnuncioLocalizacao>();
        }
    
        public int paid { get; set; }
        public string nomePais { get; set; }
    
        public virtual ICollection<tbCampanhaAnuncioLocalizacao> tbCampanhaAnuncioLocalizacao { get; set; }
    }
}
