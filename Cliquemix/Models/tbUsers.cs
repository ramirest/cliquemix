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
    
    public partial class tbUsers
    {
        public tbUsers()
        {
            this.tbAnunciante = new HashSet<tbAnunciante>();
            this.tbBan = new HashSet<tbBan>();
            this.tbLogSistema = new HashSet<tbLogSistema>();
            this.tbUsersLogAcesso = new HashSet<tbUsersLogAcesso>();
            this.tbUsersPermissao = new HashSet<tbUsersPermissao>();
        }
    
        public int uid { get; set; }
        public string username { get; set; }
        public string pwd { get; set; }
        public int utid { get; set; }
        public string cpwd { get; set; }
    
        public virtual ICollection<tbAnunciante> tbAnunciante { get; set; }
        public virtual ICollection<tbBan> tbBan { get; set; }
        public virtual ICollection<tbLogSistema> tbLogSistema { get; set; }
        public virtual ICollection<tbUsersLogAcesso> tbUsersLogAcesso { get; set; }
        public virtual ICollection<tbUsersPermissao> tbUsersPermissao { get; set; }
        public virtual tbUsersTipo tbUsersTipo { get; set; }
    }
}
