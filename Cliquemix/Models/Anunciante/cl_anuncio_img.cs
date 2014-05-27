using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anuncio_img
    {
        #region _Atributos 
            private Int32 _imgid;            //codigo da imagem;
            private Int32 _aid;              //codigo do anuncio;
            private string _anuncio_img;     //caminho da imagem;
        #endregion

        #region _Propriedades 

        public int Imgid
        {
            get { return _imgid; }
            set { _imgid = value; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public string AnuncioImg
        {
            get { return _anuncio_img; }
            set { _anuncio_img = value; }
        }
        #endregion
    }
}