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
    
    public partial class tbCalculoCondicao
    {
        public int ccid { get; set; }
        public Nullable<int> ccpid { get; set; }
        public Nullable<int> cpid { get; set; }
    
        public virtual tbCalculoCondicaoPagto tbCalculoCondicaoPagto { get; set; }
        public virtual tbCondicaoPagto tbCondicaoPagto { get; set; }
    }
}