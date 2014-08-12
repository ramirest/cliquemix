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
    
    public partial class tbAnuncio
    {
        public tbAnuncio()
        {
            this.tbAnuncioCodTemp = new HashSet<tbAnuncioCodTemp>();
            this.tbAnuncioFuncionalidade = new HashSet<tbAnuncioFuncionalidade>();
            this.tbAnuncioImg = new HashSet<tbAnuncioImg>();
            this.tbAnuncioLog = new HashSet<tbAnuncioLog>();
            this.tbCampanhaAnuncio = new HashSet<tbCampanhaAnuncio>();
            this.tbPontosRede = new HashSet<tbPontosRede>();
        }
    
        public int aid { get; set; }
        public string tituloAnuncio { get; set; }
        public string url { get; set; }
        public string dsAnuncio { get; set; }
        public string videoAnuncio { get; set; }
        public Nullable<bool> comentar { get; set; }
        public Nullable<bool> curtir { get; set; }
        public Nullable<bool> compartilhar { get; set; }
        public Nullable<int> asid { get; set; }
        public Nullable<System.DateTime> dtCriacao { get; set; }
        public Nullable<int> pid { get; set; }
        public Nullable<int> acid { get; set; }
    
        public virtual tbAnunciante tbAnunciante { get; set; }
        public virtual tbAnuncioCategoria tbAnuncioCategoria { get; set; }
        public virtual ICollection<tbAnuncioCodTemp> tbAnuncioCodTemp { get; set; }
        public virtual ICollection<tbAnuncioFuncionalidade> tbAnuncioFuncionalidade { get; set; }
        public virtual ICollection<tbAnuncioImg> tbAnuncioImg { get; set; }
        public virtual ICollection<tbAnuncioLog> tbAnuncioLog { get; set; }
        public virtual ICollection<tbCampanhaAnuncio> tbCampanhaAnuncio { get; set; }
        public virtual ICollection<tbPontosRede> tbPontosRede { get; set; }
        public virtual tbAnuncioStatus tbAnuncioStatus { get; set; }
    }
}
