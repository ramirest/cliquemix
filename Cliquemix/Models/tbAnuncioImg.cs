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
    
    public partial class tbAnuncioImg
    {
        public tbAnuncioImg()
        {
            this.tbAnuncioImagens = new HashSet<tbAnuncioImagens>();
        }
    
        public int imgid { get; set; }
        public string url_imagem { get; set; }
        public string tipo { get; set; }
        public Nullable<int> idTemp { get; set; }
        public string tamanho { get; set; }
    
        public virtual ICollection<tbAnuncioImagens> tbAnuncioImagens { get; set; }
    }
}
