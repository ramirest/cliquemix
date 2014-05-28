using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anuncio
    {
        #region _Atributos 
        private Int32 _aid; //Código do anúncio
        private string _tituloAnuncio; //Título do anúncio
        private string _url; //URL do anúncio
        private string _dsAnuncio; //Descrição do anúncio
        private string _videoAnuncio; //Caminho do vídeo do anúncio
        private Int32 _qtCliqueMax; //Qtde de cliques permitidos por consumidor
        private Int32 _aaid; //Código da área do anúncio
        #endregion

        #region _Propriedades 
        public int Aid
        {
            get { return _aid; }
        }

        public string TituloAnuncio
        {
            get { return _tituloAnuncio; }
            set { _tituloAnuncio = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string DescAnuncio
        {
            get { return _dsAnuncio; }
            set { _dsAnuncio = value; }
        }

        public string VideoAnuncio
        {
            get { return _videoAnuncio; }
            set { _videoAnuncio = value; }
        }

        public int QtdeCliqueMad
        {
            get { return _qtCliqueMax; }
            set { _qtCliqueMax = value; }
        }

        public int Aaid
        {
            get { return _aaid; }
            set { _aaid = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Novo Anúncio 
        public void _novo(string @pTituloAnuncio, string @pUrl, string @pDescAnuncio, string @pVideoAnuncio, Int32 @pQtCliqueMax, Int32 @pCodAreaAnuncio)
        {
            try
            {
                TituloAnuncio = pTituloAnuncio;
                Url = pUrl;
                DescAnuncio = pDescAnuncio;
                VideoAnuncio = pVideoAnuncio;
                QtdeCliqueMad = pQtCliqueMax;
                Aaid = pCodAreaAnuncio;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Anúncio 
        public void _editar(Int32 @pCodAnuncio, string @pTituloAnuncio, string @pUrl, string @pDescAnuncio, string @pVideoAnuncio, Int32 @pQtCliqueMax, Int32 @pCodAreaAnuncio)
        {
            try
            {
                //where aid == pCodAnuncio
                TituloAnuncio = pTituloAnuncio;
                Url = pUrl;
                DescAnuncio = pDescAnuncio;
                VideoAnuncio = pVideoAnuncio;
                QtdeCliqueMad = pQtCliqueMax;
                Aaid = pCodAreaAnuncio;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Anuncio
        public void _excluir(Int32 @pCodAnuncio)
        {
            try
            {
                //Delete SQL
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores
        public cl_anuncio() // Contrutor Padrão
        {
            _aid = 0;
            _tituloAnuncio = string.Empty;
            _url = string.Empty;
            _dsAnuncio = string.Empty;
            _videoAnuncio = string.Empty;
            _qtCliqueMax = 0;
            _aaid = 0;
        }
        #endregion

        #region _Destrutores
        public ~cl_anuncio() // Destrutor Padrão
        {
            _aid = 0;
            _tituloAnuncio = string.Empty;
            _url = string.Empty;
            _dsAnuncio = string.Empty;
            _videoAnuncio = string.Empty;
            _qtCliqueMax = 0;
            _aaid = 0;
        }
        #endregion
    }
}