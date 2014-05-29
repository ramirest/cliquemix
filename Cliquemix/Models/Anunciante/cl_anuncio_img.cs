using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anuncio_img
    {
        #region _Atributos 
        private Int32 _imgid;            //codigo da imagem do anúncio;
        private Int32 _aid;              //codigo do anuncio;
        private string _anuncioImg;     //caminho da imagem;
        #endregion

        #region _Propriedades 

        public int Imgid
        {
            get { return _imgid; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public string AnuncioImg
        {
            get { return _anuncioImg; }
            set { _anuncioImg = value; }
        }
        #endregion

        #region _Métodos 

        #region _Metodo Novo Imagem do Anúncio 
        public void _novo(Int32 @pCodAnuncio, string @pAnuncioImg)
        {
            try
            {
                Aid = pCodAnuncio;
                AnuncioImg = pAnuncioImg;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        #endregion

        #region _Metodo Editar Imagem do Anúncio 
        public void _editar(Int32 @pCodImagemAnuncio, Int32 @pCodAnuncio, string @pCaminhoImgAnuncio)
        {
            try
            {
                //where _imgid == pCodImagemAnuncio
                Aid = pCodAnuncio;
                AnuncioImg = @pCaminhoImgAnuncio;
            }
            catch (Exception)
            {                
                throw;
            }            
        }
        #endregion

        #region _Metodo Excluir Imagem do Banner
        public void _excluir(Int32 @pCodImagemAnuncio)
        {
            //Delete SQL
        }
        #endregion

        #endregion

        #region _Construtores 
        public cl_anuncio_img()
        {
            _imgid = 0;
            _aid = 0;
            _anuncioImg = string.Empty;
        }
        #endregion

        #region _ Destrutores 
        ~cl_anuncio_img()
        {
            _imgid = 0;
            _aid = 0;
            _anuncioImg = string.Empty;
        }
        #endregion
    }
}