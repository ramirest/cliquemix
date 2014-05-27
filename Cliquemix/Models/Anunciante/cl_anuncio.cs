using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anuncio
    {
        #region _Atributos 
        private Int32 _aid; //Código do anúncio
        private string _titulo_anuncio; //Título do anúncio
        private string _url; //URL do anúncio
        private string _ds_anuncio; //Descrição do anúncio
        private string _video_anuncio; //Caminho do vídeo do anúncio
        private Int32 _qtde_pode_clicar; //Qtde de cliques permitidos por consumidor
        private Int32 _aaid; //Código da área do anúncio
        #endregion

        #region _Propriedades 
        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public string TituloAnuncio
        {
            get { return _titulo_anuncio; }
            set { _titulo_anuncio = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string DsAnuncio
        {
            get { return _ds_anuncio; }
            set { _ds_anuncio = value; }
        }

        public string VideoAnuncio
        {
            get { return _video_anuncio; }
            set { _video_anuncio = value; }
        }

        public int QtdePodeClicar
        {
            get { return _qtde_pode_clicar; }
            set { _qtde_pode_clicar = value; }
        }

        public int Aaid
        {
            get { return _aaid; }
            set { _aaid = value; }
        }
        #endregion
    }
}