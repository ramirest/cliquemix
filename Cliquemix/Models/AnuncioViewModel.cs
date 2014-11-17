using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models
{
    public class AnuncioViewModel
    {
        public int Aid { get; set; }
        public string TituloAnuncio { get; set; }
        public string DsAnuncio { get; set; }
        public string VideoAnuncio { get; set; }
        public IList<tbAnuncioImg> ListaImagensAnuncio { get; set; }
        public bool Comentar { get; set; }
        public bool Curtir { get; set; }
        public bool Compartilhar { get; set; }
        public int CdAnunciante { get; set; }
        public int CdStatus { get; set; }
        public int CdCategoria { get; set; }
        public int CdCampanhaAnuncio { get; set; }
        public tbAnunciante Anunciante { get; set; }
        public tbAnuncianteEndereco EnderecoAnunciante { get; set; }
        public bool Pontuado { get; set; }
    }
}