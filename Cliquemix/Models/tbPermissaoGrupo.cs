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
    
    public partial class tbPermissaoGrupo
    {
        public tbPermissaoGrupo()
        {
            this.tbPermissao = new HashSet<tbPermissao>();
            this.tbPermissaoSubGrupo = new HashSet<tbPermissaoSubGrupo>();
        }
    
        public int pgid { get; set; }
        public string dsPermissaoGrupo { get; set; }
    
        public virtual ICollection<tbPermissao> tbPermissao { get; set; }
        public virtual ICollection<tbPermissaoSubGrupo> tbPermissaoSubGrupo { get; set; }
    }
}
