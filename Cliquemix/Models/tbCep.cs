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
    
    public partial class tbCep
    {
        public tbCep()
        {
            this.tbAnuncianteEndereco = new HashSet<tbAnuncianteEndereco>();
        }
    
        public int cepid { get; set; }
        public string dsEstado { get; set; }
        public string dsCidade { get; set; }
        public string dsLogradouro { get; set; }
        public string cep { get; set; }
        public string dsBairro { get; set; }
    
        public virtual ICollection<tbAnuncianteEndereco> tbAnuncianteEndereco { get; set; }
    }
}
