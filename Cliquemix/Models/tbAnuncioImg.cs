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
        public int imgid { get; set; }
        public int aid { get; set; }
        public string anuncioImg { get; set; }
    
        public virtual tbAnuncio tbAnuncio { get; set; }
    }
}