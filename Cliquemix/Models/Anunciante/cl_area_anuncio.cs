using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_area_anuncio
    {
        #region _Atributos 
        private Int32 aaid; //Código da área do anúncio
        private string titulo_area_anuncio; //Título da área do anúncio
        #endregion

        #region _Propriedades 
        public int Aaid
        {
            get { return aaid; }
            set { aaid = value; }
        }

        public string TituloAreaAnuncio
        {
            get { return titulo_area_anuncio; }
            set { titulo_area_anuncio = value; }
        }
        #endregion
    }
}